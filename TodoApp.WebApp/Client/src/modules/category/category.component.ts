import { Component, EventEmitter, OnInit, Output } from "@angular/core";
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

@Component({
    selector: "todo-category",
    styleUrls: ["./category.component.css"],
    templateUrl: "./category.component.html"
})


export class CategoryComponent implements OnInit {

    categories: ICategoryItem[] = [];

    @Output()
    clickViewItem: EventEmitter<number> = new EventEmitter();

    @Output()
    clickAddNewCategory = new EventEmitter<void>();

    constructor(private categoryService: CategoryService,
        public dialog: MatDialog,
        private _snackBar: MatSnackBar
        ) {
    }

    ngOnInit(): void {
        this.loadData();
    }

    async loadData(): Promise<void> {
        var allItem = await this.categoryService.getAll();
        allItem.subscribe(item => {
            this.categories = item;
        });
    }

    viewActionItem(item: ICategoryItem): void {

        this.clickViewItem.emit(item.id);
    }

    addNewCategory(): void {
        const dialogRef = this.dialog.open(AddTodoCategoryComponent, { height: '240px' });

        dialogRef.afterClosed().subscribe(result => {
            console.log(result);
        });
    }

    editCategory(item: ICategoryItem): void {

    }

    async deleteCategory(item: ICategoryItem): Promise<void> {
        var deleteRes = await this.categoryService.delete(item);
        deleteRes.subscribe(res =>{
            let notification: IMessage = {
                type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                message: res.result == Result.Success ? ["Remove succesfully"] : res.messages
            };
            this._snackBar.openFromComponent(NotificationPopupComponent, {
                data: notification,
                duration: 3000
            });
            this.loadData();
            this.clickViewItem.emit(0);
        });
    }
}