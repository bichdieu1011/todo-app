import { ModuleWithProviders, NgModule } from "@angular/core";
import { HeaderComponent } from "./components/header/header";
import { FooterComponent } from "./components/footer/footer";
import { RouterModule } from "@angular/router";
import { NotificationPopupComponent } from "./components/notification/notification.component";
import { CommonModule } from "@angular/common";
import { TaskWidgetComponent } from "./components/tasks-widget/tasks-widget.component";
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import { CdkColumnDef } from '@angular/cdk/table';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { DateDisplayPipe } from "./pipe/date.pipe";
import { NgxSpinnerService } from "ngx-spinner";
import { MySpinnerService } from "./services/spinner.service";

@NgModule({
    imports: [
        RouterModule,
        CommonModule,
        MatPaginatorModule,
        MatTableModule,
        MatSortModule,
        MatCheckboxModule
    ],
    declarations: [
        HeaderComponent, 
        FooterComponent,
        NotificationPopupComponent,
        TaskWidgetComponent,
        DateDisplayPipe
    ],
    exports: [
        HeaderComponent, 
        FooterComponent,
        NotificationPopupComponent,
        TaskWidgetComponent,
        RouterModule,
        CommonModule ,
        DateDisplayPipe
    ]
})

export class SharedModule {
    static forRoot() {
        return {
            ngModule: SharedModule,
            providers: [
                CdkColumnDef,
                NgxSpinnerService               
                
            ]
        };
    }
}