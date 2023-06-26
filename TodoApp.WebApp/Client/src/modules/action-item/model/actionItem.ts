export class IActionItem {
    public id: number;
    public categoryId: number;
    public content: string;
    public start: string;
    public end: string;
    public status: number;
    public isDone: boolean;

    constructor() {
        this.id = 0;
        this.categoryId = 0;
        this.content = '';
        this.start = '';
        this.end = '';
        this.status = 0;
        this.isDone = false;
    }
}