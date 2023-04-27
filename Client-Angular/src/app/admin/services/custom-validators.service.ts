import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { StudentsService } from './students.service';
import { StudentDTO } from '../models/StudentDTO';

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService {

  constructor(private _studentsService: StudentsService) { }

  // true if age equal or more than param age
  // below, 'minAge' param is passed, but 'control' is implicitly the input to which this validation rule is applied.
  public minAge(minAge: number, maxAge: number): ValidatorFn {

    return (control: AbstractControl): ValidationErrors | null => {

      if (!control.value) {
        return null;
      }

      let today = new Date();
      let passedDate = new Date(control.value);

      let diffInMilliSeconds = Math.abs(today.getTime() - passedDate.getTime());
      let diffInYears = (diffInMilliSeconds) / (1000 * 60 * 60 * 24 * 365.25);

      if (diffInYears >= minAge && diffInYears <= maxAge) {
        return null;
      } else {
        return { hasminage: { valid: false } };
      }
    };
  }


  // gender and country are both can't be 'Other'.
  // params sequence: gender, countryId
  public compareOthers(control1Name: string,
    control2Name: string): ValidatorFn {

    // debugger;

    return (formGroup: AbstractControl): ValidationErrors | null => {

      // let control1Value = formGroup.get(control1)?.value;
      let control1 = formGroup.get(control1Name) as FormControl;
      let control2 = formGroup.get(control2Name) as FormControl;

      if (!control1.value) {
        return null;
      }
      if (!control2.value) {
        return null;
      }

      if (control1.value == 'Other' && control2.value == '3') {

        control1.setErrors({ notBothOther: { valid: false } });
        return { notBothOther: { valid: false } };
      } else {
        control1.setErrors(null);   // important?
        return null;
      }

    };
  }


  // student id must exist. else error.
  public UniqueStudentId(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {

      let id = control.value;

      // debugger;

      return this._studentsService.getStudent(id)
        .pipe(map((response: StudentDTO | null) => {

          if (response == null) {
            // control.setErrors({ uniqueStudentId: { valid: false } }); // optional in this case 
            return { studentIdExists: { valid: false } };
          }
          else {
            return null;
          }
        }));

    };
  }
}
