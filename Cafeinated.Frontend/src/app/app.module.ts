import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


import {RouterModule, Routes} from "@angular/router";
import {WelcomeSectionComponent} from "./components/welcome-section/welcome-section.component";
import { AboutUsSectionComponent } from './components/about-us-section/about-us-section.component';
import {NavbarComponent} from "./components/navbar/navbar.component";



const routes: Routes = [
  {path: '', component: WelcomeSectionComponent},
  {path:'about-us',component: AboutUsSectionComponent}
  ]
@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    WelcomeSectionComponent,
    AboutUsSectionComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
