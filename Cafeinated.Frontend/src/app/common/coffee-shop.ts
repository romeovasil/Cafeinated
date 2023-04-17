import {Menu} from "./menu";

export class CoffeeShop {
  constructor(
    public id: number,
    public name: string,
    public photoPreviewUrl: string,
    public address: string,
    public website: string,
    public detailsImageUrl: string,
    public mapImageUrl: string,
    public menu: Menu,
    public averageRating: number) {
  }
}
