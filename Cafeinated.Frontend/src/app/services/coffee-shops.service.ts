import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {CoffeeShop} from "../common/coffee-shop";
import {Coffee} from "../common/coffee";

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


  getCoffeeListByCoffeeShopId(id: number): Observable<Coffee[]> {
    return this.http
      .get<{ coffeeShops: CoffeeShop[] }>(this.coffeeShopsUrl)
      .pipe(
        map((response) => {
          const coffeeShop = response.coffeeShops.find((shop) => shop.id === id);
          return coffeeShop ? coffeeShop.menu.coffeeList : [];
        })
      );
  }


}



interface GetResponseCoffeeShops {
  _embedded: {
    coffeeShops: CoffeeShop[];
  }
}



