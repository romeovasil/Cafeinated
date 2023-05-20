import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {CartService} from "../../services/cart.service";
import {Router} from "@angular/router";
import {Order} from "../../common/order";
import {AuthService} from "../../services/auth.service";
import {OrderService} from "../../services/order.service";

@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrls: ['./checkout-page.component.scss']
})
export class CheckoutPageComponent implements OnInit{

  checkoutFormGroup: FormGroup;
  selectedPaymentMethod: string = "cash";
  transportFee: number =7;
  totalPrice:number=0;
  totalQuantity:number=0;
  constructor(private formBuilder:FormBuilder,private cartService:CartService,private router:Router,private authService:AuthService,private orderService:OrderService) {
    this.reviewCartDetails()
  }

  ngOnInit(): void {
    this.checkoutFormGroup = this.formBuilder.group({
      shippingAddress: this.formBuilder.group({
        oras: new FormControl('', Validators.required),
        strada: new FormControl('', Validators.required),
        numar: new FormControl('', Validators.required),
        apartament: new FormControl('', Validators.required)
      }),
      paymentMethod: new FormControl('cash'),
      creditCard: this.formBuilder.group({
        cardType: new FormControl(''),
        cardName: new FormControl(''),
        cardNumber: new FormControl(''),
        cvv: new FormControl('')
      }),
    });

    this.checkoutFormGroup.get('paymentMethod')?.valueChanges.subscribe(paymentMethod => {
      const creditCardGroup = this.checkoutFormGroup.get('creditCard');
      if (creditCardGroup) {
        if (paymentMethod === 'card') {
          creditCardGroup.get('cardType')?.setValidators([Validators.required]);
          creditCardGroup.get('cardName')?.setValidators([Validators.required]);
          creditCardGroup.get('cardNumber')?.setValidators([Validators.required]);
          creditCardGroup.get('cvv')?.setValidators([Validators.required]);
        } else {
          creditCardGroup.get('cardType')?.clearValidators();
          creditCardGroup.get('cardName')?.clearValidators();
          creditCardGroup.get('cardNumber')?.clearValidators();
          creditCardGroup.get('cvv')?.clearValidators();
        }
        creditCardGroup.get('cardType')?.updateValueAndValidity();
        creditCardGroup.get('cardName')?.updateValueAndValidity();
        creditCardGroup.get('cardNumber')?.updateValueAndValidity();
        creditCardGroup.get('cvv')?.updateValueAndValidity();
      }
    });
  }




  onSubmit(){

    if (this.checkoutFormGroup.invalid) {
      console.log("invalid");
      console.log(this.selectedPaymentMethod)
      this.checkoutFormGroup.markAllAsTouched();
      return;
    } else {

      this.authService.getSession().then((session) => {
         let userId = session.userId;
        let address= this.checkoutFormGroup.get('shippingAddress.strada')?.value
          +this.checkoutFormGroup.get('shippingAddress.oras')?.value
          + this.checkoutFormGroup.get('shippingAddress.numar')?.value
          + this.checkoutFormGroup.get('shippingAddress.apartament')?.value;
        let order = new Order(userId,address,this.totalPrice,this.cartService.getCoffeeShop());
        console.log(order);
        this.orderService.saveOrder(order);
        this.checkoutFormGroup.reset();
        this.cartService.deleteCart();
        this.reviewCartDetails();
        // this.router.navigate(['/']);
        console.log("valid")

      });

    }
  }


  changePaymentMethod(paymentMethod: string) {
    this.selectedPaymentMethod=paymentMethod;
    this.checkoutFormGroup.get('paymentMethod')?.setValue('card');

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

  get cardType() { return this.checkoutFormGroup.get('creditCard.cardType'); }
  get cardName() { return this.checkoutFormGroup.get('creditCard.cardName'); }
  get cardNumber() { return this.checkoutFormGroup.get('creditCard.cardNumber'); }
  get cvv() { return this.checkoutFormGroup.get('creditCard.cvv'); }

}
