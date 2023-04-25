import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';


import {RouterModule, Routes} from "@angular/router";
import {WelcomeSectionComponent} from "./components/welcome-section/welcome-section.component";
import {AboutUsSectionComponent} from './components/about-us-section/about-us-section.component';
import {NavbarComponent} from "./components/navbar/navbar.component";
import {CafeneleSectionComponent} from './components/cafenele-section/cafenele-section.component';
import {HttpClientModule} from "@angular/common/http";
import {CoffeeShopDetailsComponent} from "./components/coffee-shop-details/coffee-shop-details.component";
import { CartSectionComponent } from './components/cart-section/cart-section.component';
import { NewCartPopUpComponent } from './components/new-cart-pop-up/new-cart-pop-up.component';
import { LoginComponent } from './components/login/login.component';



const routes: Routes = [
  {path: '', component: WelcomeSectionComponent},
  {path: 'about-us', component: AboutUsSectionComponent},
  {path: 'cafenele', component: CafeneleSectionComponent},
  {path: 'details/:id', component: CoffeeShopDetailsComponent},
  {path:'cart', component: CartSectionComponent},
  {path:'login', component: LoginComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    WelcomeSectionComponent,
    AboutUsSectionComponent,
    CafeneleSectionComponent,
    CoffeeShopDetailsComponent,
    CartSectionComponent,
    NewCartPopUpComponent,
    LoginComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
