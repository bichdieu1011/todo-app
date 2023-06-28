import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpBackend, HttpErrorResponse } from '@angular/common/http'
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { IActionItem } from "../action-item/model/actionItem";
import { IActionResultModel } from "../shared/models/IActionResult";
import { Result } from "../shared/enums/Result";
import { IActionItemList } from "./model/actionItemList";
import { WidgetType } from "../shared/enums/WidgetType";
import { UpdateActionItemStatus } from "./model/updateActionItemStatusModel";
import { ActionItemStatus } from "../shared/enums/ActionItemStatus";
import { error } from "console";
import { of } from 'rxjs';

@Injectable()
export class ActionItemService {
    private baseUrl: string = process.env['TD_BASE_URL'] as string;
    constructor(private http: HttpClient) {
    }

    async getAll(id: number): Promise<Observable<IActionItemList>> {
        let url = this.baseUrl + 'actionItem/all/' + id;

        return this.http.get(url).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async getAllByWidget(id: number, type: WidgetType, skip: number, take: number, sortBy: string, sortDirection: string): Promise<Observable<IActionItemList>> {
        let url = this.baseUrl + `actionItem/widget?categoryId=${id}&type=${type}&skip=${skip}&take=${take}&sortBy=${sortBy}&sortdirection=${sortDirection}`;

        return this.http.get(url).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    add(record: IActionItem): Observable<IActionResultModel> {
        let url = this.baseUrl + 'actionItem';

        return this.http.post(url, record).pipe(
            tap((res: any) => {
                return res;
            }),
            catchError(this.handleError)
        );
    }

    async remove(record: IActionItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'actionItem/' + record.id;

        return this.http.delete(url).pipe(
            tap((res: any) => {
                return res;
            }),
            catchError(this.handleError)
        );
    }


    async editstatus(record: IActionItem, checked: boolean): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'actionItem/editstatus/' + record.id;
        let model: UpdateActionItemStatus = {
            id: record.id,
            currentStatus: record.status,
            newStatus: checked ? ActionItemStatus.Done : ActionItemStatus.Open
        };

        return this.http.put(url, model).pipe(
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