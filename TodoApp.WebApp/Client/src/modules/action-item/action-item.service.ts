import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpBackend } from '@angular/common/http'
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
import { AuthConfig } from "../shared/auth/auth-config";


@Injectable()
export class ActionItemService {
    private baseUrl: string = process.env['TD_BASE_URL'] as string;
    private http: HttpClient
    constructor(private readonly httpHandler: HttpBackend, private authConfig: AuthConfig) {
        this.http = new HttpClient(httpHandler);

    }


    async getAll(id: number): Promise<Observable<IActionItemList>> {
        let url = this.baseUrl + 'actionItem/all/' + id;
        var header = await this.authConfig.setHeaders();

        return this.http.get(url, { headers: header }).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async getAllByWidget(id: number, type: WidgetType, skip: number, take: number, sortBy: string, sortDirection: string): Promise<Observable<IActionItemList>> {
        let url = this.baseUrl + `actionItem/widget?categoryId=${id}&type=${type}&skip=${skip}&take=${take}&sortBy=${sortBy}&sortdirection=${sortDirection}`;
        var header = await this.authConfig.setHeaders();

        return this.http.get(url, { headers: header }).pipe<IActionItemList>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async add(record: IActionItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'actionItem';
        var header = await this.authConfig.setHeaders();

        return this.http.post(url, record, { headers: header }).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }

    async remove(record: IActionItem): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'actionItem/' + record.id;
        var header = await this.authConfig.setHeaders();

        return this.http.delete(url, { headers: header }).pipe<IActionResultModel>(
            tap((res: any) => {
                return res;
            })
        );
    }


    async editstatus(record: IActionItem, checked: boolean): Promise<Observable<IActionResultModel>> {
        let url = this.baseUrl + 'actionItem/editstatus/' + record.id;
        let model: UpdateActionItemStatus = {
            id: record.id,
            currentStatus: record.status,
            newStatus: checked ? ActionItemStatus.Done : ActionItemStatus.Open
        };
        var header = await this.authConfig.setHeaders();

        return this.http.put(url, model, { headers: header }).pipe<IActionResultModel>(
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