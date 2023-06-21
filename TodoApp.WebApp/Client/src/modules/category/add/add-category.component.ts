import { Component } from "@angular/core";
import { MatBottomSheetRef} from '@angular/material/bottom-sheet';

@Component({
    selector: "add-todo-category",
    templateUrl: "./add-category.component.html",
    styleUrls:["./add-category.component.css"]
})

export class AddTodoCategoryComponent{
    constructor(private bottomSheetRef: MatBottomSheetRef<AddTodoCategoryComponent>) {

    } 

    openLink(event: MouseEvent): void {
        this.bottomSheetRef.dismiss();
        event.preventDefault();
      }
}