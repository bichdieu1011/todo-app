import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IActionItem } from "../action-item/model/actionItem";
import { IActionResultModel } from "../shared/models/IActionResult";
import { Result } from "../shared/enums/Result";
import { IActionItemList } from "./model/actionItemList";
import { WidgetType } from "../shared/enums/WidgetType";
import { UpdateActionItemStatus } from "./model/updateActionItemStatusModel";
import { ActionItemStatus } from "../shared/enums/ActionItemStatus";
import { environment } from "../../environment/environment"


@Injectable()
export class ActionItemService {
    private baseUrl: string = process.env['baseUrl'] as string;

    constructor(private http: HttpClient) { }


    getAll(id: number): Observable<IActionItemList> {
        let url = this.baseUrl + 'actionItem/all/' + id;

        return this.http.get(url).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    getAllByWidget(id: number, type: WidgetType, skip: number, take: number, sortBy: string, sortDirection: string): Observable<IActionItemList> {
        let url = this.baseUrl + `actionItem/widget?categoryId=${id}&type=${type}&skip=${skip}&take=${take}&sortBy=${sortBy}&sortdirection=${sortDirection}`;

        return this.http.get(url).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    add(record: IActionItem): Observable<IActionResultModel> {
        let url = this.baseUrl + 'actionItem';

        return this.http.post(url, record).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }

    remove(record: IActionItem): Observable<IActionResultModel> {
        let url = this.baseUrl + 'actionItem/' + record.id;

        return this.http.delete(url).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }


    editstatus(record: IActionItem, checked: boolean): Observable<IActionResultModel> {
        let url = this.baseUrl + 'actionItem/editstatus/' + record.id;
        let model: UpdateActionItemStatus = {
            id: record.id,
            currentStatus: record.status,
            newStatus: checked ? ActionItemStatus.Done : ActionItemStatus.Open
        };
        return this.http.put(url, model).pipe<IActionResultModel>(
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