import { Pipe, PipeTransform } from '@angular/core';
import { Student } from '../Student';

/*
  complex custom pipe
  Used in students-crud's *ngfor 
*/

@Pipe({
  name: 'filter',
  pure: false
})
export class FilterPipe implements PipeTransform {

  transform(studentsArr: any[], searchBy: string, searchText: string): any {

    searchBy = searchBy.toLowerCase();
    searchText = searchText.toLowerCase();

    if (studentsArr == null) {
      return studentsArr;
    }

    let resultArray = [];

    for (let student of studentsArr) {

      // debugger;

      let valueToCompare = String(student[searchBy]).toLowerCase();

      if (valueToCompare.indexOf(searchText) != -1) {
        resultArray.push(student);
      }
    }
    return resultArray;
  }


}
