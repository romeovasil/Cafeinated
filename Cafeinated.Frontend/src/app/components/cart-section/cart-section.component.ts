import { Component } from '@angular/core';
import {CoffeeShopService} from "../../services/coffee-shops.service";
import {CartService} from "../../services/cart.service";
import {CartItem} from "../../common/cart-item";
import transformJavaScript
  from "@angular-devkit/build-angular/src/builders/browser-esbuild/javascript-transformer-worker";

@Component({
  selector: 'app-cart-section',
  templateUrl: './cart-section.component.html',
  styleUrls: ['./cart-section.component.scss']
})
export class CartSectionComponent {

  cartItems : CartItem[]=[];
  totalPrice:number=0;
  totalQuantity:number=0;

  constructor(private cartService: CartService) {
  }

  ngOnInit() {
      this.listCartDetails()
  }


  private listCartDetails() {
    this.cartItems = this.cartService.cartItems;

    this.cartService.totalPrice.subscribe(
      data=>this.totalPrice = data
    );
    this.cartService.totalQuantity.subscribe(
      data=>this.totalQuantity = data
    );

    this.cartService.computeCartTotals();
  }


  removeCoffe(tempCartItem: CartItem) {
    this.cartService.remove(tempCartItem);
  }

  deleteCart() {
    this.cartService.deleteCart();
  }

  incrementQuantity(tempCartItem: CartItem) {
    this.cartService.incrementQuantity(tempCartItem)
  }

  decrementQuantity(tempCartItem: CartItem) {
    this.cartService.decrementQuantity(tempCartItem)
  }
}
