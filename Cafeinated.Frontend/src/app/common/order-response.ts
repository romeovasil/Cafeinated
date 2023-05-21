export class OrderReponse {
  constructor(
    public address: string,
    public totalPrice: number,
    public coffeeShopName: string,
    public id: string) {
  }
}
