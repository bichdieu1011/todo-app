
import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: "add-action-item",
    templateUrl: "./confirmation.component.html",
    styleUrls:["./confirmation.component.css"]
})

export class ConfirmationDialogComponent implements OnInit {
    constructor(public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: string) {
    }

    ngOnInit(): void {
    }

    async onYesClick(): Promise<void> {
        this.dialogRef.close({ type: 'yes' });
    }

    onNoClick(): void {
        this.dialogRef.close();
    }
}