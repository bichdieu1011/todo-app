import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ICategoryItem } from "./model/categoryItem.model";
import { IActionResultModel } from "../shared/models/IActionResult";
import { Result } from "../shared/enums/Result";
import { environment } from "../../environment/environment"
import {  } from "node:process";
@Injectable()
export class CategoryService {
    private baseUrl: string = process.env['baseUrl'] as string;
    constructor(private http: HttpClient) { }


    getAll(): Observable<ICategoryItem[]> {
        let url = this.baseUrl + 'category/all';

        return this.http.get(url).pipe<ICategoryItem[]>(
            tap((res: any) => {
                return res;
            })
        );
    }

    add(record: ICategoryItem): Observable<IActionResultModel> {
        let url = this.baseUrl + 'category';

        return this.http.post(url, record).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }

    delete(record: ICategoryItem): Observable<IActionResultModel> {
        let url = this.baseUrl + 'category/' + record.id;

        return this.http.delete(url).pipe<IActionResultModel>(
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