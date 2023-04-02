import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

/*
Age cant be less than five.
*/

@Directive({
  selector: '[appAgeValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: AgeValidatorDirective, multi: true }]
})
export class AgeValidatorDirective implements Validator {

  constructor() { }

  // @Input('appAgeValidator') n: string = '2023';

  validate(control: AbstractControl): ValidationErrors | null {

    debugger;

    let inputYear: string = control.value.substring(0, 4);
    let currYear: string = '2023';
    
    let isValid = (currYear !== inputYear);

    let t = false;

    // if (t) {
    //   return null;
    // } else {
    //   return { correctAge: { valid: false } }; // errors.correctAge
    // }
    
    return { correctAge: { valid: false } }; // errors.correctAge
  }


}
