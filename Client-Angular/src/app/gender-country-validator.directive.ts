import { Directive } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

/*
Gender,Country both can't be Other.
*/

@Directive({
  selector: '[appGenderCountryValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: GenderCountryValidatorDirective, multi: true }]
})
export class GenderCountryValidatorDirective implements Validator {

  constructor() { }

  
  validate(control: AbstractControl): ValidationErrors | null {
    
    // debugger;
    // in html, name='gender', name='countryId'.
    if (control.value.gender === 'other' && control.value.countryId === '3'){      
      return { bothOthers: { valid: false } };  // errors.bothOthers
    }
    else  {
      return null;
    }
  }

}
