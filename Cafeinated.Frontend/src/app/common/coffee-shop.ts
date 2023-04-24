import {Coffee} from "./coffee";

export class CoffeeShop {
  constructor(
    public id: string,
    public name: string,
    public photoPreviewUrl: string ,
    public address: string,
    public website: string,
    public mapImageUrl: string ,
    public coffeeList:Coffee[],
    public averageRating: number) {
  }
}
