import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../common/order";
import {OrdersComponent} from "../components/orders/orders.component";
import {firstValueFrom, Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {OrderReponse} from '../common/order-response';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private orderUrl = environment.apiUrl + 'api/order';

  constructor(private http:HttpClient) {
  }

  saveOrder(order: Order) {
    return firstValueFrom(this.http.post<Order>(this.orderUrl,order));
  }

  getOrdersByUserId(userId:string): Observable<OrderReponse[]>{
    let url = this.orderUrl + `?userId=${userId}`;
    return this.http.get<OrderReponse[]>(url);
  }
}
