import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IActionItem } from "../action-item/model/actionItem";
import { ICategoryItem } from "./model/categoryItem.model";

@Injectable()
export class CategoryService {
    private baseUrl: string = 'https://localhost:7142/';
    constructor(private http: HttpClient) { }


    getAll(): Observable<ICategoryItem[]> {
        let url = this.baseUrl + '/category/all';

        return this.http.get(url).pipe<ICategoryItem[]>(
            tap((res: any) => {                
                return res;
            })
        );
    }
}