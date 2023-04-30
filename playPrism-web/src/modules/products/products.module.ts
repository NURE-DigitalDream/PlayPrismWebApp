import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import {ProductsRoutingModule} from "./products-routing.module";
import {ProductsService} from "../../core/services";
import {SharedModule} from "../shared/shared.module";

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    SharedModule
  ],
  providers: [ProductsService]
})
export class ProductsModule { }
