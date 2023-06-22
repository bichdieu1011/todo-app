import { Component, OnInit } from "@angular/core";
import { ActionItemService } from "./action-item.service";
import { IColumn } from "../shared/models/IColumn";
import { IActionItem } from "./model/actionItem";
import { WidgetType } from "../shared/enums/WidgetType";
import { Sort } from "@angular/material/sort";
import { PageEvent } from "@angular/material/paginator";
import { IActionItemList } from "./model/actionItemList";
import { MatDialog } from "@angular/material/dialog";
import { AddActionItemComponent } from "./add/add-action-item.component";
import { FieldType } from "../shared/enums/FieldType";
import { MySpinnerService } from "../shared/services/spinner.service";
import { NgxSpinnerService } from 'ngx-spinner';
import { WidgetDetails } from "../shared/models/WidgetDetails";
import { IMessage } from "../shared/models/IMessage";
import { NotificationType } from "../shared/enums/NotificationType";
import { Result } from "../shared/enums/Result";
import { NotificationPopupComponent } from "../shared/components/notification/notification.component";
import { MatSnackBar } from "@angular/material/snack-bar";


@Component({
    selector: 'todo-actionItem',
    styleUrls: ['./action-item.component.css'],
    templateUrl: './action-item.component.html'
})

export class ActionItemComponent implements OnInit {
    today: IActionItemList = {} as IActionItemList;
    expired: IActionItemList = {} as IActionItemList;
    tomorrow: IActionItemList = {} as IActionItemList;
    thisWeek: IActionItemList = {} as IActionItemList;


    widgetDetails: WidgetDetails[] = [];

    categoryId: number = 0;
    columnsToDisplay: IColumn[] = [
        { displayName: "", fieldName: "isDone", sortable: false, type: FieldType.checkbox },
        { displayName: "Content", fieldName: "content", sortable: true, type: FieldType.text },
        { displayName: "Start Date", fieldName: "start", sortable: true, type: FieldType.datetime },
        { displayName: "Deadline", fieldName: "end", sortable: true, type: FieldType.datetime }
    ];

    constructor(private actionItemService: ActionItemService,
        public dialog: MatDialog,
        private _snackBar: MatSnackBar,
        private spinner: MySpinnerService) {

    }

    ngOnInit(): void {
        this.widgetDetails = [
            { pageSize: 3, pageIndex: 0, sortby: "", sortDirection: "" },
            { pageSize: 3, pageIndex: 0, sortby: "", sortDirection: "" },
            { pageSize: 3, pageIndex: 0, sortby: "", sortDirection: "" },
            { pageSize: 3, pageIndex: 0, sortby: "", sortDirection: "" }
        ];
    }

    onViewListActionItem(id: number): void {
        this.spinner.show();
        this.categoryId = id;
        this.loadWidget(WidgetType.Today, this.widgetDetails[WidgetType.Today]);
        this.loadWidget(WidgetType.Tomorrow, this.widgetDetails[WidgetType.Today]);
        this.loadWidget(WidgetType.ThisWeek, this.widgetDetails[WidgetType.Today]);
        this.loadWidget(WidgetType.Expired, this.widgetDetails[WidgetType.Today]);
    }

    loadWidget(type: WidgetType, details: WidgetDetails) {
        this.actionItemService.getAllByWidget(this.categoryId, type, details.pageSize * details.pageIndex, details.pageSize, details.sortby, details.sortDirection).subscribe((item: IActionItemList) => {
            switch (type) {
                case WidgetType.Today:
                    this.today = item;
                    return;
                case WidgetType.Tomorrow:
                    this.tomorrow = item;
                    return;
                case WidgetType.ThisWeek:
                    this.thisWeek = item;
                    return;
                case WidgetType.Expired:
                default:
                    this.expired = item;
                    return;
            }
        });
    }

    addActionItem(): void {
        this.spinner.show();
        const dialogRef = this.dialog.open(AddActionItemComponent, { height: '500px', width: '500px', data: this.categoryId });

        dialogRef.afterClosed().subscribe(result => {
            this.onViewListActionItem(this.categoryId);
        });
    }

    changeSorting(event: Sort, type: WidgetType): void {
        this.widgetDetails[type].sortby = event.active;
        this.widgetDetails[type].sortDirection = event.direction;
        this.loadWidget(type, this.widgetDetails[type]);
    }

    changePaging(event: PageEvent, type: WidgetType): void {
        if (event.previousPageIndex != event.pageIndex) {
            this.widgetDetails[type].pageIndex = event.pageIndex;
        }
        else {
            this.widgetDetails[type].pageIndex = 0;
            this.widgetDetails[type].pageSize = event.pageSize;

        }
        this.loadWidget(type, this.widgetDetails[type]);
    }

    edit(event: IActionItem, type: WidgetType): void {
        this.widgetDetails[type].pageIndex = 0;
        this.loadWidget(type, this.widgetDetails[type]);
    }

    remove(event: IActionItem, type: WidgetType): void {
        this.actionItemService.remove(event).subscribe(res => {
            let notification: IMessage = {
                type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                message: res.result == Result.Success ? ["Remove succesfully"] : res.messages
            };
            this._snackBar.openFromComponent(NotificationPopupComponent, {
                data: notification,
                duration: 3000
            });

            this.widgetDetails[type].pageIndex = 0;
            this.loadWidget(type, this.widgetDetails[type]);
        });

    }

    onChangeCheckbox(event: any, type: WidgetType): void {
        var isChecked = event.checked;
        var item = event.data;
        var fieldName = event.fieldName;
        this.widgetDetails[type].pageIndex = 0;
        this.actionItemService.editstatus(event.data, isChecked).subscribe(res => {

            let notification: IMessage = {
                type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                message: res.result == Result.Success ? ["Update succesfully"] : res.messages
            };
            this._snackBar.openFromComponent(NotificationPopupComponent, {
                data: notification,
                duration: 3000
            });
            this.loadWidget(type, this.widgetDetails[type]);
        });


    }

}