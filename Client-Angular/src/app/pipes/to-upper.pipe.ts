import { Pipe, PipeTransform } from '@angular/core';

/*
  custom pipe.
  Used in students-crud
*/


@Pipe({
  name: 'toUpperPipe'
})
export class ToUpperPipe implements PipeTransform {

  // coountry name to uppercase
  transform(value: string, ...args: unknown[]): unknown {
    if (value == null) {
      return null;
    } else {
      return value.toUpperCase() + ' (' + args[0] + args[1] + ')'; // better do string interpolation
    }

  }

}
