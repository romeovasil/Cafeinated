import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../common/order";
import {OrdersComponent} from "../components/orders/orders.component";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private orders: Order[]=[];
  private orderUrl:string="";
  constructor(private http:HttpClient) { }

  saveOrder(order: Order) {
    this.orders.push(order);
    return  this.http.post<Order>(this.orderUrl,order);

  }

  getOrders() {
    let order1 = new Order("123","cetinei 2 timisoara",125,"Tucano","111")
    let order2 = new Order("345","cetinei 3 timisoara",855,"Starbucks","222")
    let order3 = new Order("456","cetinei 4 timisoara",79,"Vintage","333")
    this.orders.push(order1);
    this.orders.push(order2);
    this.orders.push(order3);
    return this.orders;
  }

  getOrdersByUserId(userId:string){
    let url = this.orderUrl;
    return this.http.get<Order[]>(url);
  }
}
