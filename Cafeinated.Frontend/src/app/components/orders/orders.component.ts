import {Component, OnInit} from '@angular/core';
import {Order} from "../../common/order";
import {OrderService} from "../../services/order.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{
  orders:Order[]=[];

  ngOnInit(): void {
    // this.authService.getSession().then((session) => {
    //   let userId = session.userId;
    //   this.orderService.getOrdersByUserId(userId).subscribe(
    //     data => this.orders = data
    //   );
    // });

    this.orders=this.orderService.getOrders();
  }

  constructor(private orderService:OrderService,private authService:AuthService) {
  }


  addOrder(order:Order){
    this.orders.push(order);
    console.log(this.orders);
  }
}
