import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { ICategoryList } from "./model/category.list.model";
import { ICategoryItem } from "./model/categoryItem.model";
import { CategoryService } from "./category.service";
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog, DialogPosition, MatDialogConfig } from '@angular/material/dialog';
import { AddTodoCategoryComponent } from "./add/add-category.component";



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
        public dialog: MatDialog) {
    }




    ngOnInit(): void {
        this.loadData();
    }

    loadData(): void {
        // this.categories = [{ id: 1, name: "Item 1" }, { id: 2, name: "Item 2" }];
        // return;
        this.categoryService.getAll().subscribe(item => {
            this.categories = item;
        });
    }

    viewActionItem(item: ICategoryItem): void {

        this.clickViewItem.emit(item.id);
    }

    addNewCategory(): void {
        // this.clickAddNewCategory.emit();
        // this.bottomSheet.open(AddTodoCategoryComponent);

        const dialogRef = this.dialog.open(AddTodoCategoryComponent, { height: '240px' });

        dialogRef.afterClosed().subscribe(result => {
            console.log(result);
        });
    }

    editCategory(item: ICategoryItem): void {

    }

    deleteCategory(item: ICategoryItem): void {

    }
}