import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppRoutingModule } from './app.routes';
import { HomeModule } from './home/home.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/share.module';
import { CategoryModule } from './category/category.module';
import { ActionItemModule } from './action-item/action-item.module';
import { CdkColumnDef } from '@angular/cdk/table';
import { NgxSpinnerService } from "ngx-spinner";
import { MsalModule, MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { MsalInterceptor } from '@azure/msal-angular';
import { } from "node:process";

const isIE = window.navigator.userAgent.indexOf('MSIE') > -1 || window.navigator.userAgent.indexOf('Trident') > 1;

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
    AppRoutingModule,
    MsalModule.forRoot(new PublicClientApplication
      (
        {
          auth: {
            clientId: process.env['TD_CLIENT_ID'] as string,
            redirectUri: process.env['TD_REDIRECT_URL'],
            authority: 'https://login.microsoftonline.com/'+ (process.env['TD_APP_ID'] as string)
          },
          cache: {
            cacheLocation: 'localStorage',
            storeAuthStateInCookie: isIE
          }
        }
      ),
      {
        interactionType: InteractionType.Redirect,
        authRequest: {
          scopes: ['user.read']
        }
      },
      {
        interactionType: InteractionType.Redirect,
        protectedResourceMap: new Map(
          [
            ["https://graph.microsoft.com/v1.0/me", ['user.Read']]
          ]
        )
      }
    )
  ],
  providers: [
    CdkColumnDef,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    MsalGuard
  ],
  bootstrap: [
    AppComponent,
    MsalRedirectComponent
  ]
})
export class AppModule {


}
