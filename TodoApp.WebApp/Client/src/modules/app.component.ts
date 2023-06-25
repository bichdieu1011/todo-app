import { Component, OnInit, Inject, OnDestroy } from "@angular/core";
import {
    MsalGuardConfiguration,
    MSAL_GUARD_CONFIG,
    MsalBroadcastService,
    MsalService
} from '@azure/msal-angular'
import { InteractionStatus, RedirectRequest } from "@azure/msal-browser";
import { Subject, filter, takeUntil } from "rxjs";
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
        if (this.msalGuardConfig.authRequest) {
            this.authService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest)
        }

    }
    ngOnInit(): void {
        this.msalBroadCastService.inProgress$.pipe(
            filter((interactionStatus: InteractionStatus) => interactionStatus == InteractionStatus.None),
            takeUntil(this._destroy)
        ).subscribe(x => {
            this.isUserLogin = this.authService.instance.getAllAccounts().length > 0
        });
    }

    logout(): void {
        this.authService.logoutRedirect();
    }

    ngOnDestroy(): void {
        this._destroy.next(undefined);
        this._destroy.complete();
    }



}