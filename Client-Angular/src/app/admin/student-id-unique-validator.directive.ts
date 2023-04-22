import { Directive } from '@angular/core';
import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { StudentsService } from '../students.service';
import { Student } from '../Student';

/*
  async custom vali in Template Driven Form.
  Used in: Create Student.
  Student Id must not exist already.
*/

@Directive({
  selector: '[appStudentIdUniqueValidator]',
  providers: [{ provide: NG_ASYNC_VALIDATORS, useExisting: StudentIdUniqueValidatorDirective, multi: true }]
})
export class StudentIdUniqueValidatorDirective implements AsyncValidator {

  constructor(private studentsService: StudentsService) { }

  validate(control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {

    return this.studentsService.getStudent(control.value).pipe(map(
      (existing: Student) => {

        debugger;

        if (existing == null) {
          return null; // no error
        } else {
          return { uniqueStudentId: { valid: false } };
        }

      }
    ));
  }



}
