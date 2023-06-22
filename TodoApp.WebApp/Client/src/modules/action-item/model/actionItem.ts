export class IActionItem {
    public id: number;
    public categoryId: number;
    public content: string;
    public start: Date;
    public end: Date;
    public status: number;
    public isDone: boolean;

    constructor() {
        this.id = 0;
        this.categoryId = 0;
        this.content = '';
        this.start = new Date();
        this.end = new Date();
        this.status = 0;
        this.isDone = false;
    }
}