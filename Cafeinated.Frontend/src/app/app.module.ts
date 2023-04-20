import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';


import {RouterModule, Routes} from "@angular/router";
import {WelcomeSectionComponent} from "./components/welcome-section/welcome-section.component";
import {AboutUsSectionComponent} from './components/about-us-section/about-us-section.component';
import {NavbarComponent} from "./components/navbar/navbar.component";
import {CafeneleSectionComponent} from './components/cafenele-section/cafenele-section.component';
import {HttpClientModule} from "@angular/common/http";
import {CoffeeShopDetailsComponent} from "./components/coffee-shop-details/coffee-shop-details.component";
import { CartSectionComponent } from './components/cart-section/cart-section.component';


const routes: Routes = [
  {path: '', component: WelcomeSectionComponent},
  {path: 'about-us', component: AboutUsSectionComponent},
  {path: 'cafenele', component: CafeneleSectionComponent},
  {path: 'details/:id', component: CoffeeShopDetailsComponent},
  {path:'cart',component:CartSectionComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    WelcomeSectionComponent,
    AboutUsSectionComponent,
    CafeneleSectionComponent,
    CoffeeShopDetailsComponent,
    CartSectionComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
