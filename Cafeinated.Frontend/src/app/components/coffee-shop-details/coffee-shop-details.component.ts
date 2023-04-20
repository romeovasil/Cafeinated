import {Component, Input, OnInit} from '@angular/core';
import {CoffeeShopService} from "../../services/coffee-shops.service";
import {CoffeeShop} from "../../common/coffee-shop";
import {Coffee} from "../../common/coffee";


@Component({
  selector: 'app-coffee-shop-details',
  templateUrl: './coffee-shop-details.component.html',
  styleUrls: ['./coffee-shop-details.component.scss']
})
export class CoffeeShopDetailsComponent implements OnInit {

  @Input() coffeeShop!: CoffeeShop;
  coffeeList: Coffee[] = [];
  showPopup = false;

  constructor(private coffeeShopService: CoffeeShopService) {


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


}
