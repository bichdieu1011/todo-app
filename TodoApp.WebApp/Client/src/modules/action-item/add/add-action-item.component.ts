import { Component, EventEmitter, Inject, OnInit, Output } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { NotificationPopupComponent } from "src/modules/shared/components/notification/notification.component";
import { IMessage } from "src/modules/shared/models/IMessage";
import { NotificationType } from "src/modules/shared/enums/NotificationType";
import { Result } from "src/modules/shared/enums/Result";
import { ActionItemService } from "../action-item.service";
import { IActionItem } from "../model/actionItem";
import { MySpinnerService } from "src/modules/shared/services/spinner.service";

@Component({
    selector: "add-action-item",
    templateUrl: "./add-action-item.component.html",
    styleUrls: ["./add-action-item.component.css"]
})

export class AddActionItemComponent implements OnInit {
    record: IActionItem;

    @Output()
    afterAddNewItem = new EventEmitter();
    newItemForm: FormGroup;
    constructor(public dialogRef: MatDialogRef<AddActionItemComponent>,
        @Inject(MAT_DIALOG_DATA) public data: number,
        private actionItemService: ActionItemService,
        private _snackBar: MatSnackBar,
        private spinner: MySpinnerService,
        private fb: FormBuilder) {

        this.record = new IActionItem();
        this.record.categoryId = data;
        this.newItemForm = this.fb.group({
            'content': [this.record.content, Validators.required],
            'start': [this.record.start, Validators.required],
            'end': [this.record.end, Validators.required],
        },{validators : [this.checkDates,this.checkString]});
        
    }

    ngOnInit(): void {
        
    }

    checkDates(group: FormGroup) {
        if (group.controls['end'].value < group.controls['start'].value) {
            return { notValid: true }
        }
        return null;
    }

    checkString(group: FormGroup) {
        if (group.controls['content'].value.trim().length == 0) {
            return { notValid: true }
        }
        return null;
    }

    onSaveClick(): void {
        this.spinner.show();
        this.record.content = this.newItemForm.controls['content'].value.trim();
        this.record.start = this.newItemForm.controls['start'].value;
        this.record.end = this.newItemForm.controls['end'].value;

        this.actionItemService.add(this.record).subscribe(res => {
            let notification: IMessage = {
                type: res.result == Result.Error ? NotificationType.Error : NotificationType.Information,
                message: res.result == Result.Success ? ["Save succesfully"] : res.messages
            };
            this._snackBar.openFromComponent(NotificationPopupComponent, {
                data: notification,
                duration: 3000
            });
            this.afterAddNewItem.emit();
            this.dialogRef.close();
            this.spinner.hide();
        });

    }

    onNoClick(): void {
        this.dialogRef.close();

    }

   
}