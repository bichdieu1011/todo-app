import { ModuleWithProviders, NgModule } from "@angular/core";
import { HeaderComponent } from "./components/header/header";
import { FooterComponent } from "./components/footer/footer";
import { RouterModule } from "@angular/router";


@NgModule({
    imports: [
        RouterModule
    ],
    declarations: [
        HeaderComponent, 
        FooterComponent
    ],
    exports: [
        HeaderComponent, 
        FooterComponent,
        RouterModule
    ]
})

export class SharedModule {
    static forRoot() {
        return {
            ngModule: SharedModule,
            providers: [
               
            ]
        };
    }
}