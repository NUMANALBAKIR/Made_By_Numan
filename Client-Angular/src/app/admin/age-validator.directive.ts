import { Directive, Input } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appAgeValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: AgeValidatorDirective, multi: true }]
})
export class AgeValidatorDirective implements Validator {

  constructor() { }

  @Input('appAgeValidator') n: number = 0;

  validate(control: AbstractControl): ValidationErrors | null {

    let currYear: number = Number(this.n);
    let inputYear: number = Number(control.value.substring(0, 4));

    let isValid = (currYear != inputYear);

    debugger;

    let t = false;

    if (t) {
      return null;
    } else {
      return { correctAge: { valid: false } }; // errors.correctAge
    }

  }


}
