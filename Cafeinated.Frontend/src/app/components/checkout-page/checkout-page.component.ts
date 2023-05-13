import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {CartService} from "../../services/cart.service";

@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrls: ['./checkout-page.component.scss']
})
export class CheckoutPageComponent implements OnInit{

  checkoutFormGroup: FormGroup;
  selectedPaymentMethod: string ;
  transportFee: number =7;
  totalPrice:number=0;
  totalQuantity:number=0;
  constructor(private formBuilder:FormBuilder,private cartService:CartService) {
    this.reviewCartDetails()
  }

  ngOnInit(): void {
    this.checkoutFormGroup = this.formBuilder.group({
      shippingAddress:this.formBuilder.group({
        oras: new FormControl('', Validators.required),
        strada:new FormControl('', Validators.required),
        numar:new FormControl('', Validators.required),
        apartament:new FormControl('', Validators.required)

      }),
      creditCard:this.formBuilder.group({
        cartType:[''],
        cardName:[''],
        cardNumber:[''],
        cvv:['']

      }),
    });
  }


  onSubmit(){
      if(this.checkoutFormGroup.invalid){
        console.log("invalid");
        this.checkoutFormGroup.markAllAsTouched();
        return;
      }
      else{
        console.log("valid")
      }
  }


  changePaymentMethod(paymentMethod: string) {
    this.selectedPaymentMethod=paymentMethod;
  }



  private reviewCartDetails() {

    this.cartService.totalPrice.subscribe(
      data=>this.totalPrice = data +this.transportFee
    );
    this.cartService.totalQuantity.subscribe(
      data=>this.totalQuantity = data
    );

    this.cartService.computeCartTotals();
  }

  get strada() {
    return this.checkoutFormGroup.get('shippingAddress.strada'); }
  get oras() { return this.checkoutFormGroup.get('shippingAddress.oras'); }
  get numar() { return this.checkoutFormGroup.get('shippingAddress.numar'); }
  get apartament() { return this.checkoutFormGroup.get('shippingAddress.apartament'); }


}
