import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

/*
  NOT working.
  cross field vali  in Template Driven Form. 
  Used in: Create Student.
  Gender, Country both can't be Other.
*/

@Directive({
  selector: '[appGenderCountryValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: GenderCountryValidatorDirective, multi: true }]
})
export class GenderCountryValidatorDirective implements Validator {

  validate(control: AbstractControl): ValidationErrors | null {

    // debugger;

    let countryIdValue = control.value.countryId; // name atribute is 'countryId'
    let genderValue = control.value.gender;  // name="gender"

    if (countryIdValue == '3' && genderValue == 'Other') { // if invalid
      return { bothOther: { valid: false } };
    }
    else {
      return null; // if valid
    }
  }

}
