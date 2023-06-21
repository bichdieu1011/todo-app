import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "../shared/share.module";
import { CategoryComponent } from "./category.component";
import { CategoryService } from "./category.service";
import { HttpClientModule } from "@angular/common/http";
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [BrowserModule,
        SharedModule,
        HttpClientModule,
        MatBottomSheetModule,
        BrowserAnimationsModule],
    declarations: [CategoryComponent],
    providers: [CategoryService],
    exports: [CategoryComponent]
})

export class CategoryModule { }