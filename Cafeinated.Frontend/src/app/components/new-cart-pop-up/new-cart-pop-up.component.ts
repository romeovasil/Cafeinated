import { Component } from '@angular/core';
import {CartItem} from "../../common/cart-item";
import {CoffeeShopService} from "../../services/coffee-shops.service";
import {CartService} from "../../services/cart.service";

@Component({
  selector: 'app-new-cart-pop-up',
  templateUrl: './new-cart-pop-up.component.html',
  styleUrls: ['./new-cart-pop-up.component.scss']
})
export class NewCartPopUpComponent {
  showPopup = false;
  cartItem:CartItem;

  constructor(private cartService: CartService) {
  }

  ngOnInit(): void {

  }

  openPopup(theCartItem: CartItem) {
    this.showPopup = true;
    this.cartItem=theCartItem
  }

  closePopup() {
    this.showPopup = false;
  }

  addToNewCart() {
    this.cartService.addToCart(this.cartItem);
    this.closePopup();

  }
}
