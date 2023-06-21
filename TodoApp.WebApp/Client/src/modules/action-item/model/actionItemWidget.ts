import { IActionItem } from "./actionItem";

export interface IActionItemWidget {
    today: IActionItem[];
    tomorrow: IActionItem[];
    thisWeek: IActionItem[];
    expired: IActionItem[];
}