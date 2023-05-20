import {Component, OnInit} from '@angular/core';
import {Order} from "../../common/order";
import {OrderService} from "../../services/order.service";

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{
  orders:Order[]=[];

  ngOnInit(): void {
  }

  constructor(private orderService:OrderService) {
      this.orderService.getOrders().subscribe(
        data => this.orders=data
      );
  }
}
