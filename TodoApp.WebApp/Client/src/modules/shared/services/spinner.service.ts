import { Injectable } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";

@Injectable({
    providedIn: 'root',
  })
export class MySpinnerService {
    showIndex = 0;
    hideIndex = 0;
  
    constructor(private spinner: NgxSpinnerService) {}
  
    show() {
      this.showIndex++;
      this.spinner.show();
    //   console.log('show spinner', this.showIndex);
    }
  
    hide() {
      this.hideIndex++;
      if (this.showIndex === this.hideIndex) {
        this.spinner.hide();
        // console.log('hide spinner', this.hideIndex);      
      }
    }
}