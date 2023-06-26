import { Component, EventEmitter, OnInit, Output, OnDestroy } from "@angular/core";
import { ICategoryList } from "./model/category.list.model";
import { ICategoryItem } from "./model/categoryItem.model";
import { CategoryService } from "./category.service";
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog, DialogPosition, MatDialogConfig } from '@angular/material/dialog';
import { AddTodoCategoryComponent } from "./add/add-category.component";
import { MatSnackBar } from "@angular/material/snack-bar";
import { IMessage } from "../shared/models/IMessage";
import { Result } from "../shared/enums/Result";
import { NotificationType } from "../shared/enums/NotificationType";
import { NotificationPopupComponent } from "../shared/components/notification/notification.component";
import { NgxSpinner, NgxSpinnerService } from "ngx-spinner";
import { ConfirmationDialogComponent } from "../shared/components/confirmation-dialog/confirmation.component";

@Component({
    selector: "todo-category",
    styleUrls: ["./category.component.css"],
    templateUrl: "./category.component.html"
})


export class CategoryComponent implements OnInit, OnDestroy {

    categories: ICategoryItem[] = [];
    selected: number = 0;

    @Output()
    clickViewItem: EventEmitter<number> = new EventEmitter();

    @Output()
    clickAddNewCategory = new EventEmitter<void>();

    constructor(private categoryService: CategoryService,
        public dialog: MatDialog,
        private _snackBar: MatSnackBar,
        private _spinner: NgxSpinnerService
    ) {
    }

    ngOnInit(): void {
        this.loadData();
    }

    async loadData(): Promise<void> {
        this._spinner.show();
        var allItem = await this.categoryService.getAll();
        allItem.subscribe(item => {
            this.categories = item;
            this._spinner.hide();
        });
    }

    viewActionItem(item: ICategoryItem): void {
        this.selected = item.id;
        this.clickViewItem.emit(item.id);
    }

    addNewCategory(): void {
        const dialogRef = this.dialog.open(AddTodoCategoryComponent, { height: '240px' });

        dialogRef.afterClosed().subscribe(result => {
            if (result.type)
                this.loadData();
        });
    }

    editCategory(item: ICategoryItem): void {

    }

    async deleteCategory(item: ICategoryItem): Promise<void> {
        const dialogRef = this.dialog.open(ConfirmationDialogComponent, { data: `Are you sure to delete category ${item.name}. This action could not be undo!` });

        dialogRef.afterClosed().subscribe(async (result) => {
            if (!result.type) return;

            this._spinner.show();
            var deleteRes = await this.categoryService.delete(item);
            deleteRes.subscribe(res => {
                let notification: IMessage = {
                    type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                    message: res.result == Result.Success ? ["Remove succesfully"] : res.messages
                };
                this._snackBar.openFromComponent(NotificationPopupComponent, {
                    data: notification,
                    duration: 3000
                });
                this.selected = 0;
                this.loadData();
                this.clickViewItem.emit(0);
            });
        });

    }

    ngOnDestroy(): void {
        this.categories = [];
        this.selected = 0;

    }
}