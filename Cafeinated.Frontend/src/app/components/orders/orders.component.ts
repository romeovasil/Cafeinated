import {Component, OnInit} from '@angular/core';
import {Order} from "../../common/order";
import {OrderService} from "../../services/order.service";
import {AuthService} from "../../services/auth.service";
import {OrderReponse} from '../../common/order-response';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit{
  orders:OrderReponse[]=[];

  async ngOnInit() {
    const session = await this.authService.getSession();
    this.orderService.getOrdersByUserId(session.userId).subscribe(data => {
      this.orders = data;
    })
  }

  constructor(private orderService:OrderService,private authService:AuthService) {
  }
}
