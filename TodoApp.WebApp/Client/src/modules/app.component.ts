import { Component, OnInit, Inject, OnDestroy } from "@angular/core";
import {
    MsalGuardConfiguration,
    MSAL_GUARD_CONFIG,
    MsalBroadcastService,
    MsalService
} from '@azure/msal-angular'
import { AuthenticationResult, EventMessage, EventType, InteractionStatus, RedirectRequest } from "@azure/msal-browser";
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


    }
    async ngOnInit(): Promise<any> {

        // this.msalBroadCastService.msalSubject$.pipe(
        //     filter((ms: EventMessage) => ms.eventType == EventType.LOGIN_SUCCESS
        //         || ms.eventType == EventType.SSO_SILENT_SUCCESS)
        // )
        //     .subscribe((res: EventMessage) => {
        //         const payload = res.payload as AuthenticationResult;
        //         this.authService.instance.setActiveAccount(payload.account);
        //     });

        this.msalBroadCastService.inProgress$
            .pipe(filter((status: InteractionStatus) => status === InteractionStatus.None)
                , takeUntil(this._destroy))
            .subscribe(async () => {
                await this.authService.instance.initialize();
                this.isUserLogin = this.authService.instance.getAllAccounts().length > 0
                if (!this.isUserLogin) {
                    await this.logIn();
                }
            });


        // this.msalBroadCastService.inProgress$.pipe(
        //     filter((interactionStatus: InteractionStatus) => interactionStatus == InteractionStatus.None),
        //     takeUntil(this._destroy)
        // ).subscribe(x => {
        //     this.isUserLogin = this.authService.instance.getAllAccounts().length > 0
        // });
    }

    async logIn(): Promise<void> {

        if (this.msalGuardConfig.authRequest) {
            // await this.authService.instance.initialize();
            // this.authService.instance.handleRedirectPromise({})
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