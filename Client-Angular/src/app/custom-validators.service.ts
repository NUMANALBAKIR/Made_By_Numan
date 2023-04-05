import { Injectable } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorsService {

  constructor() { }

  // true if age equal or more than param age
  // below, 'minAge' param is passed, but 'control' is implicitly the input to which this validation rule is applied.
  public minAge(minAge: number): ValidatorFn {

    return (control: AbstractControl): ValidationErrors | null => {

      if (!control.value) {
        return null;
      }

      let today = new Date();
      let passedDate = new Date(control.value);

      let diffInMilliSeconds = Math.abs(today.getTime() - passedDate.getTime());
      let diffInYears = (diffInMilliSeconds) / (1000 * 60 * 60 * 24 * 365.25);

      if (diffInYears >= minAge) {
        return null;
      } else {
        return { hasminage: { valid: false } };
      }
    };
  }
  

  // true if gender and country are both 'Other'.
  // params sequence: gender, countryId
  public compareOthers(control1Name: string,
    control2Name: string): ValidatorFn {

    // debugger;

    return (formGroup: AbstractControl): ValidationErrors | null => {

      // let control1Value = formGroup.get(control1)?.value;
      let control1 = formGroup.get(control1Name) as FormControl;
      let control2 = formGroup.get(control2Name) as FormControl;

      // if (!control1.value) {
      //   return null;
      // }
      // if (!control2.value) {
      //   return null;
      // }

      if (control1.value == 'Other' && control2.value == '3') {

        control1.setErrors({ notBothOther: { valid: false } });
        return { notBothOther: { valid: false } };
      } else {
        control1.setErrors( null);   // important?
        return null;
      }

    };
  }

}
