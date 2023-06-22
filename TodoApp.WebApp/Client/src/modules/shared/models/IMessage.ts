import { NotificationType } from "../enums/NotificationType";

export interface IMessage{
    type: NotificationType;
    message : string[];
}