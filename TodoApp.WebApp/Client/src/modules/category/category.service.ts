import { Injectable } from "@angular/core";
import { HttpClient, HttpBackend, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { ErrorObserver, Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ICategoryItem } from "./model/categoryItem.model";
import { IActionResultModel } from "../shared/models/IActionResult";
import { Result } from "../shared/enums/Result";
import { environment } from "../../environment/environment"
import { } from "node:process";
@Injectable()
export class CategoryService {
    private baseUrl: string = process.env['TD_BASE_URL'] as string;

    constructor(
        private http: HttpClient) {
    }


    async getAll(): Promise<Observable<ICategoryItem[]>> {
        let url = this.baseUrl + 'category/all';
        return this.http.get(url).pipe(
            tap((res: any) => {
                return res;
            })
        );
    }

    async add(record: ICategoryItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'category';

        return this.http.post(url, record).pipe(
            tap((res: any) => {
                return res;
            }),
            catchError(this.handleError)
        );
    }

    async delete(record: ICategoryItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'category/' + record.id;

        return this.http.delete(url).pipe(
            tap((res: any) => {
                return res;
            }),
            catchError(this.handleError)
        );
    }

    private handleError(error: HttpErrorResponse): Observable<IActionResultModel> {
        console.log('Backend - ' +
            `status: ${error.status}, ` +
            `statusText: ${error.statusText}, ` +
            `message: ${error.error.errors}`);

        var messages = error.error.errors[Object.keys(error.error.errors)[0]];
        var res: IActionResultModel = { result: Result.Error, messages: messages };

        return of(res);
    }
}