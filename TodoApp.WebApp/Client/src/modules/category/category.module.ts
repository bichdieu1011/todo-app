import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "../shared/share.module";
import { CategoryComponent } from "./category.component";
import { CategoryService } from "./category.service";
import { HttpClientModule } from "@angular/common/http";
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule } from '@angular/forms';

import { AddTodoCategoryComponent } from "./add/add-category.component";

@NgModule({
    imports: [BrowserModule,
        SharedModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatBottomSheetModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule,
        FormsModule
    ],
    declarations: [CategoryComponent, AddTodoCategoryComponent],
    providers: [CategoryService],
    exports: [CategoryComponent, AddTodoCategoryComponent]
})

export class CategoryModule { }