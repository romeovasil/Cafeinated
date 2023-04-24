import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {CoffeeShopService} from "../../services/coffee-shops.service";
import {CoffeeShop} from "../../common/coffee-shop";
import {Coffee} from "../../common/coffee";
import {CartService} from "../../services/cart.service";
import {CartItem} from "../../common/cart-item";

import {NewCartPopUpComponent} from "../new-cart-pop-up/new-cart-pop-up.component";


@Component({
  selector: 'app-coffee-shop-details',
  templateUrl: './coffee-shop-details.component.html',
  styleUrls: ['./coffee-shop-details.component.scss']
})
export class CoffeeShopDetailsComponent implements OnInit {

  @Input() coffeeShop!: CoffeeShop;
  coffeeList: Coffee[] = [];
  showPopup = false;
  @ViewChild('newCartPopUp') newCartPopUp!: NewCartPopUpComponent;


  constructor(private coffeeShopService: CoffeeShopService , private cartService: CartService) {


  }

  ngOnInit(): void {

  }

  openPopup() {
    this.coffeeList = this.coffeeShop.coffeeList;
    this.showPopup = true;
  }

  closePopup() {
    console.log("pressed")
    this.showPopup = false;
    console.log(this.showPopup);
  }

  addToCart(theCoffee: Coffee, coffeeShop: CoffeeShop)
  {

    const theCartItem = new CartItem(theCoffee,coffeeShop)
    if(this.cartService.cartItems.length==0)
    {
      this.cartService.addToCart(theCartItem);
      console.log("Aceeasi cafenea");
    }
    else{
      if(this.cartService.cartItems[0].coffeeShop.id!=coffeeShop.id)
      {
        this.newCartPopUp.openPopup(theCartItem);
      }
      else{
        this.cartService.addToCart(theCartItem);
        console.log("Aceeasi cafenea");
      }
    }

  }

}
