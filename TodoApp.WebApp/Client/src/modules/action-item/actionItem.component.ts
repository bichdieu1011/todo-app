import { Component, OnInit } from "@angular/core";


@Component({
    selector: 'todo-actionItem',
    styleUrls: ['./actionItem.component.css'],
    templateUrl: './actionItem.component.html'
})

export class ActionItemComponent implements OnInit {
    
    constructor(){

    }

    ngOnInit(): void {
        
    }

    onViewListActionItem(id : number): void{
        console.log(id);
    }

}