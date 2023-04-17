import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {CoffeeShop} from "../common/coffee-shop";

@Injectable({
  providedIn: 'root'
})
export class CoffeeShopService {
  // private baseUrl="";
  private coffeeShopsUrl = 'assets/mock-data/coffee-shops.json';

  constructor(private http: HttpClient) {
  }

  getCoffeeShops(): Observable<CoffeeShop[]> {
    return this.http.get<{ coffeeShops: CoffeeShop[] }>(this.coffeeShopsUrl)
      .pipe(
        map(response => response.coffeeShops)
      );

    // return this.http.get<GetResponseCoffeeShops>(this.baseUrl).pipe(map(response => response._embedded.coffeeShops));

  }


}



interface GetResponseCoffeeShops {
  _embedded: {
    coffeeShops: CoffeeShop[];
  }
}



