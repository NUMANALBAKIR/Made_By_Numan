import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

/*
  custom vali  in Template Driven Form. 
  Used in: Create Student.
  Age must be between 6 and 18.
*/

@Directive({
  selector: '[appAgeValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: AgeValidatorDirective, multi: true }]
})
export class AgeValidatorDirective implements Validator {

  @Input('appAgeValidator') n: string ='';
 
  validate(control: AbstractControl): ValidationErrors | null {

    if (control.value == null) {
      return null;
    }
 
    let inputYear: string = control.value.substring(0, 4);
    let currYear: string = this.n; 

    debugger;
    
    let difference = Number(currYear) - Number(inputYear);

    if (difference >= 6 && difference <= 18) {
      return null;  // no errors
    } else {
      return { correctAge: { valid: false } }; // errors?.correctAge
    }
  }




}
