import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { routing } from './app.routes';
import { HomeModule } from './home/home.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/share.module';
import { CategoryModule } from './category/category.module';
import { ActionItemModule } from './action-item/action-item.module';
import { CdkColumnDef } from '@angular/cdk/table';
import { NgxSpinnerService } from "ngx-spinner";


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HomeModule,
    SharedModule,
    CategoryModule,    
    ActionItemModule,
    HttpClientModule,
    routing
  ],
  providers: [CdkColumnDef],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {

  
 }
