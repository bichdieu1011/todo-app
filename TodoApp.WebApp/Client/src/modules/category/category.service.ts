import { Injectable } from "@angular/core";
import { HttpClient, HttpBackend, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ICategoryItem } from "./model/categoryItem.model";
import { IActionResultModel } from "../shared/models/IActionResult";
import { Result } from "../shared/enums/Result";
import { environment } from "../../environment/environment"
import { } from "node:process";
import { AuthConfig } from "../shared/auth/auth-config";
@Injectable()
export class CategoryService {
    private baseUrl: string = process.env['TD_BASE_URL'] as string;


    private http: HttpClient
    constructor(private readonly httpHandler: HttpBackend,
        private authConfig: AuthConfig) {
        this.http = new HttpClient(httpHandler);
    }


    async getAll(): Promise<Observable<ICategoryItem[]>> {
        let url = this.baseUrl + 'category/all';
        var header = await this.authConfig.setHeaders();
        return this.http.get(url, { headers: header }).pipe<ICategoryItem[]>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async add(record: ICategoryItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'category';
        var header = await this.authConfig.setHeaders();

        return this.http.post(url, record, { headers: header }).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async delete(record: ICategoryItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'category/' + record.id;
        var header = await this.authConfig.setHeaders();

        return this.http.delete(url, { headers: header }).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }

    private handleError(error: any): any {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('Client side network error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error('Backend - ' +
                `status: ${error.status}, ` +
                `statusText: ${error.statusText}, ` +
                `message: ${error.error.message}`);
        }

        // return an observable with a user-facing error message
        var res: IActionResultModel = { result: Result.Error, messages: ["An error occurs!"] };
        return res;
    }
}