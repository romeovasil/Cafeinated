import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
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
        oras:[''],
        strada:[''],
        numar:[''],
        apartament:['']

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
    console.log(this.checkoutFormGroup.get('shippingAddress')?.value);
    console.log(this.selectedPaymentMethod)
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
}
