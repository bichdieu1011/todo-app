import { Component, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { IMessage } from '../../models/IMessage';
import { NotificationType } from '../../enums/NotificationType';

@Component({
    selector: 'notification-popup',
    templateUrl: "./notification.component.html",
    styleUrls: ["./notification.component.css"]
})
export class NotificationPopupComponent {
    icon: string;
    messages: string;
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: IMessage) {

        switch (data.type) {
            case NotificationType.Information:
                this.icon = "information";
                break;
            case NotificationType.Warning:
                this.icon = "warning";
                break;
            case NotificationType.Error:
                this.icon = "error";
                break;
        }


        this.messages = data.message.join("<br />");
    }
}