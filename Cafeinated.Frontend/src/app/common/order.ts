import {Coffee} from "./coffee";

export class Order {
  constructor(
              public userId: string,
              public address: string,
              public totalPrice: number,
              public CoffeeShopName: string,
              public id?: string) {

  }
}
