import { NgModule }             from '@angular/core';
import { BrowserModule  }       from '@angular/platform-browser';
import { CommonModule }         from '@angular/common'
import { HomeComponent } from './home-component';
import { SharedModule } from '../shared/share.module';

@NgModule({
    imports: [BrowserModule, SharedModule, CommonModule],
    declarations: [HomeComponent],
    // providers: [CatalogService]
})
export class HomeModule { }
