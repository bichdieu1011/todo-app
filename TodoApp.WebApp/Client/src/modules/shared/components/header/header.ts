import { Component, Input } from '@angular/core';

@Component({
    selector: 'todo-header',
    templateUrl: './header.html',
    styleUrls: ['./header.css']
})
export class HeaderComponent {
    @Input()
    url: string | undefined;
}
