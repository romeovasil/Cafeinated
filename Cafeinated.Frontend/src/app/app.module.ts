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
import { CheckoutPageComponent } from './components/checkout-page/checkout-page.component';
import {FormsModule} from "@angular/forms";
import {ReactiveFormsModule} from "@angular/forms";
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './components/register/register.component';
import { OrderComponent } from './common/order/order.component';



const routes: Routes = [
  {path: '', component: WelcomeSectionComponent},
  {path: 'about-us', component: AboutUsSectionComponent},
  {path: 'cafenele', component: CafeneleSectionComponent},
  {path: 'details/:id', component: CoffeeShopDetailsComponent},
  {path:'cart', component: CartSectionComponent},
  {path:'login', component: LoginComponent},
  {path:'checkout',component: CheckoutPageComponent}
]

const matImports = [
  MatSnackBarModule,
  MatFormFieldModule,
  MatInputModule,
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
    LoginComponent,
    CheckoutPageComponent,
    RegisterComponent,
    OrderComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    ...matImports,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
