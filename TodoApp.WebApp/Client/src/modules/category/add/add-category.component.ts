import { Component, EventEmitter, Output } from "@angular/core";
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CategoryService } from "../category.service";
import { ICategoryItem } from "../model/categoryItem.model";
import { NotificationPopupComponent } from "src/modules/shared/components/notification/notification.component";
import { IMessage } from "src/modules/shared/models/IMessage";
import { NotificationType } from "src/modules/shared/enums/NotificationType";
import { Result } from "src/modules/shared/enums/Result";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: "add-todo-category",
    templateUrl: "./add-category.component.html",
    styleUrls: ["./add-category.component.css"]
})

export class AddTodoCategoryComponent {

    newCategoryForm: FormGroup;
    @Output()
    afterAddNewCategory = new EventEmitter();

    constructor(public dialogRef: MatDialogRef<AddTodoCategoryComponent>,
        private categoryService: CategoryService,
        private _snackBar: MatSnackBar,
        private fb: FormBuilder) {

        this.newCategoryForm = this.fb.group({
            'categoryName': ['', Validators.required]
        }, { validators: this.checkString });
    }

    checkString(group: FormGroup) {
        if (group.controls['categoryName'].value.trim().length == 0) {
            return { notValid: true }
        }
        return null;
    }


    onSaveClick(): void {
        // save

        let record: ICategoryItem = { id: 0, name: this.newCategoryForm.controls['categoryName'].value.trim() };
        this.categoryService.add(record).subscribe(res => {
            let notification: IMessage = {
                type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                message: res.result == Result.Success ? ["Save succesfully"] : res.messages
            };
            this._snackBar.openFromComponent(NotificationPopupComponent, {
                data: notification,
                duration: 3000
            });
            this.afterAddNewCategory.emit();
            this.dialogRef.close();
        });

    }

    onNoClick(): void {
        this.dialogRef.close();

    }
}