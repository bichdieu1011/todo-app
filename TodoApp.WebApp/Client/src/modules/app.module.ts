import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppRoutingModule } from './app.routes';
import { HomeModule } from './home/home.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/share.module';
import { CategoryModule } from './category/category.module';
import { ActionItemModule } from './action-item/action-item.module';
import { CdkColumnDef } from '@angular/cdk/table';
import { MsalModule, MsalGuard, MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { MsalInterceptor } from '@azure/msal-angular';
import { } from "node:process";
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

const isIE = window.navigator.userAgent.indexOf('MSIE') > -1 || window.navigator.userAgent.indexOf('Trident') > 1;

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule ,
    NgxSpinnerModule ,

    BrowserModule,
    AppRoutingModule,
    HomeModule,
    SharedModule,
    CategoryModule,
    ActionItemModule,
    HttpClientModule,
    MsalModule.forRoot(new PublicClientApplication
      (
        {
          auth: {
            // clientId: process.env['TD_CLIENT_ID'] as string,
            // redirectUri: process.env['TD_REDIRECT_URL'],
            // authority: 'https://login.microsoftonline.com/'+ (process.env['TD_APP_ID'] as string)

            clientId: process.env['TD_CLIENT_ID'] as string,
            redirectUri: process.env['TD_REDIRECT_URL'],
            authority: 'https://login.microsoftonline.com/' + (process.env['TD_APP_ID'] as string)
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
            ["https://graph.microsoft.com/v1.0/me", ['user.Read']],
            [(process.env['TD_BASE_URL'] as string), [(process.env['TD_API_SCOPE'] as string)]]

          ]
        )
      }
    )
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
    MsalGuard,
    CdkColumnDef,
  ],
  bootstrap: [
    AppComponent,
    MsalRedirectComponent
  ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule {


}
