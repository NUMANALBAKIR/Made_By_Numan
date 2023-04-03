import { Directive } from '@angular/core';
import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { Observable } from 'rxjs';
import { StudentsService } from './students.service';
import { map } from 'rxjs/operators';
import { Student } from './Student';

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
          return { uniqueId: { valid: false } };
        }

      }
    ));
  }



}
