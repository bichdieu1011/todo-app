import { Component, OnInit, Inject, OnDestroy } from "@angular/core";
import {
    MsalGuardConfiguration,
    MSAL_GUARD_CONFIG,
    MsalBroadcastService,
    MsalService
} from '@azure/msal-angular'
import { AuthenticationResult, EventMessage, EventType, InteractionStatus, RedirectRequest } from "@azure/msal-browser";
import { Subject, filter, takeUntil } from "rxjs";
import { CategoryService } from "./category/category.service";
@Component({
    selector: 'todo-app',
    styleUrls: ['./app.component.css'],
    templateUrl: './app.component.html'
})

export class AppComponent implements OnInit, OnDestroy {
    isUserLogin: boolean = false;
    private readonly _destroy = new Subject<void>();

    constructor(
        @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
        private msalBroadCastService: MsalBroadcastService,
        private authService: MsalService
    ) {


    }
     ngOnInit(): any {

        this.msalBroadCastService.inProgress$
            .pipe(filter((status: InteractionStatus) => status === InteractionStatus.None)
                , takeUntil(this._destroy))
            .subscribe(async () => {
                this.isUserLogin = this.authService.instance.getAllAccounts().length > 0
                    if (!this.isUserLogin) {
                        await this.logIn();
                    }
                
            });

    }

   async logIn(): Promise<void> {

        if (this.msalGuardConfig.authRequest) {
           await this.authService.instance.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
        }
        else {
           await this.authService.instance.loginRedirect();
        }
    }

    logout(): void {
        this.authService.logoutRedirect();
    }

    ngOnDestroy(): void {
        this._destroy.next(undefined);
        this._destroy.complete();
    }
}