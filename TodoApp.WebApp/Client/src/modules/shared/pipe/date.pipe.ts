import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
        name: 'dateDisplay'
})
export class DateDisplayPipe implements PipeTransform {
    transform(value: Date) {
        let currentDate = new Date(value);
        return currentDate.toDateString();
    }
}
