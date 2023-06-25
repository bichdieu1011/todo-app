import {
    MsalGuardConfiguration,
    MSAL_GUARD_CONFIG,
    MsalBroadcastService,
    MsalService
} from '@azure/msal-angular'
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpBackend } from '@angular/common/http'
import { AuthenticationResult, InteractionRequiredAuthError, SilentRequest } from '@azure/msal-browser';

@Injectable()
export class AuthConfig {
    constructor(private authService: MsalService) {

    }

    public async setHeaders(): Promise<any> {
        const httpOptions = {
            headers: new HttpHeaders()
        };
        let accounts = this.authService.instance.getAllAccounts();
        if (accounts.length > 0) {
            var silentRequest: SilentRequest = {
                scopes: ['user.Read'],
                account: accounts[0]
            };
            try {
                var res = await this.authService.instance.acquireTokenSilent(silentRequest);
                httpOptions.headers = httpOptions.headers.set('Authorization', `Bearer ${res.accessToken}`);
                return httpOptions;

            }
            catch (error: any) {
                if (error instanceof InteractionRequiredAuthError) {
                    this.authService.instance
                        .acquireTokenPopup(silentRequest)
                        .then(function (accessTokenResponse) {                        // Call your API with token
                            httpOptions.headers = httpOptions.headers.set('Authorization', `Bearer ${accessTokenResponse.accessToken}`);

                        })
                        .catch(function (error) {
                            // Acquire token interactive failure
                            console.log(error);
                        });
                }
            }

        }


    }
}
