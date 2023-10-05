import { Directive } from '@angular/core';
import { AbstractControl, AsyncValidator, NG_ASYNC_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Student } from '../admin/models/Student';
import { StudentsService } from '../admin/services/students.service';
import { StudentDTO } from '../admin/models/StudentDTO';

/*
  works
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

  validate(control: AbstractControl): Observable<ValidationErrors | null> {

    return this.studentsService.getStudent(control.value).pipe(map(
      (existing: StudentDTO| any) => {
        if (existing != null) {
          return { uniqueStudentId: { valid: false } };
        }
        else {
          return null;
        }
      }));
      
  }



}
