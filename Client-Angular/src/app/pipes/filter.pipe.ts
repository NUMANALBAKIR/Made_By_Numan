import { Pipe, PipeTransform } from '@angular/core';

/*
  complex custom pipe
  Used in students-crud's *ngfor 
*/

@Pipe({
  name: 'filterPipe',
  pure: false
})
export class FilterPipe implements PipeTransform {

  transform(studentsArr: any[], searchBy: string, searchText: string): any {

    searchBy = searchBy.trim().toLowerCase();
    searchText = searchText.trim().toLowerCase();

    if (studentsArr == null || searchText == '') {
      return studentsArr;
    }

    let resultArray = [];

    for (let student of studentsArr) {

      let valueToCompare = String(student[searchBy]).toLowerCase(); // note String and lowercase

      if (valueToCompare.indexOf(searchText) != -1) {   // collection.indexOf(alphabet)
        resultArray.push(student);
      }
    }
    return resultArray;
  }


}
