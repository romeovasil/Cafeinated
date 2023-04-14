import { Component, OnInit } from '@angular/core';
import {CoffeeShop} from "../../common/coffee-shop";
import {CoffeeShopService} from "../../services/coffee-shops.service";


@Component({
  selector: 'app-cafenele-section',
  templateUrl: './cafenele-section.component.html',
  styleUrls: ['./cafenele-section.component.scss']
})
export class CafeneleSectionComponent implements OnInit {

  coffeeShops: CoffeeShop[] = [];

  constructor(private coffeShopService: CoffeeShopService) { }

  ngOnInit() {
    this.listProducts();
  }

  listProducts() {
    this.coffeShopService.getCoffeeShops().subscribe(
      data => {
        this.coffeeShops = data;
      }
    )
  }

}
