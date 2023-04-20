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

  openPopup(currentId: string) {
    let tempId = currentId;
    this.coffeeShopService.getCoffeeListByCoffeeShopId(tempId).subscribe(
      (tempCoffeeList) => {
        this.coffeeList = tempCoffeeList;
      },
      (error) => {
        console.error('Error fetching coffee menu:', error);
      }
    );
    this.showPopup = true;
  }

  closePopup() {
    console.log("pressed")
    this.showPopup = false;
    console.log(this.showPopup);
  }


}
