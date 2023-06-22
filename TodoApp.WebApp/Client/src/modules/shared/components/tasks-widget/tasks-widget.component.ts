import { Component, OnInit, OnChanges, Input, Output, EventEmitter } from "@angular/core";
import { IColumn } from "../../models/IColumn";
import { Sort } from "@angular/material/sort";
import { PageEvent } from "@angular/material/paginator";
import { MatCheckboxChange } from "@angular/material/checkbox";

@Component({
    selector: 'tasks-widget',
    styleUrls: ['./tasks-widget.component.css'],
    templateUrl: './tasks-widget.component.html'
})

export class TaskWidgetComponent implements OnInit, OnChanges {
    @Input()
    title: string = '';

    @Input()
    columns: IColumn[] = [];

    @Input()
    pageSizeOptions: number[] = [3,5];

    @Input()
    pageSize: number = 3;

    @Input()
    length: number = 0;

    @Input()
    dataSource: any[] = [];

    @Input()
    isEditable: boolean = false;

    @Input()
    isRemovable: boolean = false;


    @Output()
    onChangSorting = new EventEmitter<Sort>();

    @Output()
    onChangPaging = new EventEmitter<PageEvent>();

    @Output()
    onEditing = new EventEmitter<any>();

    @Output()
    onRemoving = new EventEmitter<any>();

    @Output()
    onChangeCheckbox = new EventEmitter<any>();

    displayColumns: string[] = [];

    constructor() {

    }

    ngOnInit(): void {
        this.displayColumns = [];
        for (let item in this.columns) {
            this.displayColumns.push(this.columns[item].fieldName);
        }
        if(this.isEditable || this.isRemovable)
        this.displayColumns.push("action");
    }

    ngOnChanges(): void {

    }

    edit(item: any): void {
        this.onEditing.emit(item);
    }
    remove(item: any): void {
        this.onRemoving.emit(item);
    }

    sortData(event: Sort): void {
        this.onChangSorting.emit(event);
    }

    changePaging(event: PageEvent): void {
        this.onChangPaging.emit(event);
    }

    changeCheckbox(event: MatCheckboxChange, item: any, fieldName: string){
        this.onChangeCheckbox.emit({checked: event.checked, data: item, field: fieldName});
    }
}