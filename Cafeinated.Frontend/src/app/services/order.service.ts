import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../common/order";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private orderUrl:string="";
  constructor(private http:HttpClient) { }

  saveOrder(order: Order) {
    return  this.http.post<Order>(this.orderUrl,order);

  }
}
