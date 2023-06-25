import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home-component';
import { MsalGuard } from '@azure/msal-angular';
import { AppComponent } from './app.component';



export const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [MsalGuard]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabledBlocking'
  })],
  exports: [RouterModule]
})


export class AppRoutingModule {

}

