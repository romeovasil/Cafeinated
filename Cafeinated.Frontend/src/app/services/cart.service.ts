import { Injectable } from '@angular/core';
import {CartItem} from "../common/cart-item";
import {BehaviorSubject, Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CartService {

  cartItems: CartItem[]=[];
  totalPrice: Subject<number> = new BehaviorSubject<number>(0);
  totalQuantity: Subject<number> = new BehaviorSubject<number>(0);
  coffeeShopName:string="";
  coffeeShopId: string = '';
  storage:Storage = localStorage;
  constructor() {
    let data =JSON.parse(this.storage.getItem('cartItems')!);
    if ( data !=null)
    {
      this.cartItems = data;
      this.persistCartItems();
    }
  }
  public addToCart(theCartItem:CartItem){
    let alreadyExistsInCart: boolean = false;
    let existingCartItem: CartItem = undefined!;

    if (this.cartItems.length > 0) {
      existingCartItem = this.cartItems.find(tempCartItem => tempCartItem.id == theCartItem.id)!;
    }
    alreadyExistsInCart = (existingCartItem != undefined);
    if (alreadyExistsInCart) {
      existingCartItem.quantity++;
    }
    else {
      if(this.cartItems.length>0)
      {
        this.coffeeShopName=this.cartItems[0].coffeeShop.name;

        if(this.cartItems[0].coffeeShop.id==theCartItem.coffeeShop.id){
          this.cartItems.push(theCartItem);
        }
        else{
          this.cartItems.splice(0,this.cartItems.length);
          this.cartItems.push(theCartItem);
        }
      }
      else{
        this.cartItems.push(theCartItem);
      }


    }

    this.computeCartTotals();
  }







  computeCartTotals() {

    let totalPriceValue: number = 0;
    let totalQuantityValue: number = 0;

    for (let currentCartItem of this.cartItems) {
      totalPriceValue += currentCartItem.quantity * currentCartItem.unitPrice;
      totalQuantityValue += currentCartItem.quantity;
    }

    // publish the new values ... all subscribers will receive the new data
    this.totalPrice.next(totalPriceValue);
    this.totalQuantity.next(totalQuantityValue);


    this.persistCartItems()

  }

  persistCartItems(){
    this.storage.setItem('cartItems',JSON.stringify(this.cartItems));
  }

  getCartItems() :CartItem[]{
    return this.cartItems;

  }


  remove(theCartItem: any) {
    const itemIndex = this.cartItems.findIndex(tempCartItem => theCartItem.id ===tempCartItem.id);

    if(itemIndex>-1)
    {
      this.cartItems.splice(itemIndex,1);
      this.computeCartTotals();
    }
  }

  deleteCart() {
    this.cartItems.splice(0,this.cartItems.length);
    this.computeCartTotals();
  }

  incrementQuantity(theCartItem: CartItem) {
    theCartItem.quantity++;
    this.computeCartTotals();
  }
  decrementQuantity(theCartItem: CartItem) {
    theCartItem.quantity--;
    if(theCartItem.quantity<1){
      this.remove(theCartItem);
    }
    this.computeCartTotals();
  }

  getCoffeeShopId(){
    return this.cartItems[0].coffeeShop.id;
  }
}
