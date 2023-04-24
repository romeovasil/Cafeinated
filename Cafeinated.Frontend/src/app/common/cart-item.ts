import {Coffee} from "./coffee";
import {CoffeeShop} from "./coffee-shop";

export class CartItem {


  id: string;
  name: string;
  unitPrice: number;
  coffeeShop: CoffeeShop;
  quantity: number;

  constructor(coffee: Coffee , coffeeShop:CoffeeShop) {
    this.id = coffee.id;
    this.name = coffee.name;
    this.unitPrice = coffee.pricePerUnit;
    this.quantity = 1;
    this.coffeeShop=coffeeShop;
  }


}
