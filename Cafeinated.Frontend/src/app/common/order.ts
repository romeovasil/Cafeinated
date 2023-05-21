export class Order {
  constructor(
              public applicationUserId: string,
              public address: string,
              public totalPrice: number,
              public coffeeShopId: string,
              public paymentMethod: string,
              public id?: string) {
  }
}
