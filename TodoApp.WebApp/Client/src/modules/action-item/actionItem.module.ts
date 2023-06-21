import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { SharedModule } from "../shared/share.module";
import { HttpClientModule } from "@angular/common/http";
import { ActionItemComponent } from "./actionItem.component";

@NgModule({
    imports: [BrowserModule, SharedModule, HttpClientModule],
    declarations: [ActionItemComponent],
    providers: [],
    exports: [ActionItemComponent]
})

export class ActionItemModule { }