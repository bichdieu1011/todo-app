import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "../shared/share.module";
import { HttpClientModule } from "@angular/common/http";
import { ActionItemComponent } from "./action-item.component";
import { ActionItemService } from "./action-item.service";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { AddActionItemComponent } from "./add/add-action-item.component";
import { MySpinnerService } from "../shared/services/spinner.service";
import { NgxSpinnerService } from 'ngx-spinner';
@NgModule({
    imports: [
        BrowserModule,
        SharedModule,
        HttpClientModule,
        BrowserAnimationsModule,
        MatBottomSheetModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule,
        FormsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        ReactiveFormsModule
    ],
    declarations: [ActionItemComponent, AddActionItemComponent],
    providers: [ActionItemService, NgxSpinnerService],
    exports: [ActionItemComponent]
})

export class ActionItemModule { }