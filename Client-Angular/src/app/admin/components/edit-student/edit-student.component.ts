import { Component, OnDestroy, OnInit } from '@angular/core';
import { StudentsService } from 'src/app/admin/services/students.service';
import { StudentUpdateDTO } from 'src/app/admin/models/StudentUpdateDTO';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/admin/models/country';
import { Router } from '@angular/router';
import { StudentDTO } from 'src/app/admin/models/StudentDTO';
import { Observable, Subscription } from 'rxjs';
import { Student } from '../../models/Student';
import { ICanDeactivate } from '../../guards/can-deactivate-guard.service';
import { CountriesService } from '../../services/countries.service';
import { CustomValidatorsService } from '../../services/custom-validators.service';


/*
    Reactive Form
*/
// issue: patched submit doesnt hitting API endpoint

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit, OnDestroy, ICanDeactivate {

  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private customValidatorsService: CustomValidatorsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.studentId = this.studentsService.studentIdPassed;
    this.genders = ['Female', 'Male', 'Other'];
    this.studentUpdateDTO = new StudentUpdateDTO();
    this.submitted = false;
    this.subscriptions = [];
    this.canLeave = true;
  }

  studentId: number;
  countries!: Observable<Country[]>;
  genders: string[];  // for dynamic radio buttons
  studentUpdateDTO: StudentUpdateDTO;
  submitted: boolean;
  subscriptions: Subscription[];
  canLeave: boolean;



  // way 1. (validators like ways 2 below can also be applied here.)
  // updateFormReactve: FormGroup = new FormGroup({
  //   studentId: new FormControl(this.studentUpdateDTO.studentId),
  //   name: new FormControl(),
  //   dateOfBirth: new FormControl(),
  //   passed: new FormControl(),
  //   gender: new FormControl(),
  //   countryId: new FormControl(),
  //   subjects: new FormArray([])
  // });

  // way 2: using formBuilder
  updateFormReactve: FormGroup = this.formBuilder.group(
    {
      studentId: [null, [Validators.required, Validators.pattern(/^[0-9]*$/)], [this.customValidatorsService.UniqueStudentId()], { updateOn: 'blur' }],
      name: [null, [Validators.required, Validators.minLength(3), Validators.pattern(/^[A-Za-z ]+$/)]],
      dateOfBirth: [null, [Validators.required, this.customValidatorsService.minAge(6, 18)]],
      passed: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      countryId: [null, [Validators.required]],
      subjects: this.formBuilder.array([])  // note ( [] )
    },
    {
      validators: [
        this.customValidatorsService.compareOthers('gender', 'countryId')
      ]
    }
  );


  ngOnInit() {

    // get, set list using async pipe bcoz no processing needed. and auto subscribes.
    this.countries = this.countriesService.getCountries();

    // get, set student info by id
    this.subscriptions.push(

      this.studentsService.getStudent(this.studentId).subscribe(
        (response: StudentDTO) => {
          this.studentUpdateDTO = response;
          this.populateForm();
        },
        (e) => {
          console.log('-- From editComponent: ' + e);
        })
      ,
      this.updateFormReactve.valueChanges.subscribe(
        (value) => {
          this.canLeave = false;
          // console.log(value);
        })
        // ,this.updateFormReactve.controls.name.valueChanges.subscribe(
        //   (v)=>{
        //     alert(v);
        //   }
        // )
    );

    // no need of below, because done above in subscribe's arrow function
    // wait for service responses and then populate.
    // setTimeout(() => {
    //   this.populateForm();
    // }, 500);

    this.updateFormReactve.reset(); // note
  }


  get formSubjectsArr() {   // note
    return this.updateFormReactve.get('subjects') as FormArray;
    // return <FormArray>this.updateFormReactve.get('subjects'); //alternatve
  }


  onAddClick() {

    // way 1. way 2 is below
    // var newFormGroup = new FormGroup({
    //   subjectName: new FormControl(),
    //   mark: new FormControl()
    // });

    // way 2
    var newFormGroup = this.formBuilder.group({   // think of a formGroup like a class
      subjectName: [null, [Validators.required, Validators.minLength(3)]],
      mark: [50, [Validators.required]],
      studentId: this.updateFormReactve.value.studentId
    });

    this.formSubjectsArr.push(newFormGroup);

    if (this.formSubjectsArr.valid) {
    }

  }


  onRemoveClick(i: number) {
    this.formSubjectsArr.removeAt(i);
  }


  onSubmitClick() {

    this.submitted = true;

    if (this.updateFormReactve.valid) {
      this.studentUpdateDTO = this.updateFormReactve.value as StudentUpdateDTO; // note

      this.subscriptions.push(
        this.studentsService.updateStudent(this.studentUpdateDTO).subscribe(
          (r: Student) => {
          },
          (e) => {
            console.log(e);
          })
      );
      this.canLeave = true;

      // better alternaive is router below
      // this.location.back();

      // .navigate is preferred to .navigateByUrl
      this.router.navigate(['/admin', 'studentscrud']); // ['/parent', 'child']
    }

  }


  onResetClick() {

    // this.updateFormReactve.reset();

    // or,
    this.updateFormReactve.reset({
      passed: false
    });

    this.formSubjectsArr.reset();
  }


  // populate form
  private populateForm(): void {
    this.updateFormReactve.patchValue({
      studentId: this.studentUpdateDTO.studentId,
      name: this.studentUpdateDTO.name,
      dateOfBirth: this.studentUpdateDTO.dateOfBirth,
      passed: this.studentUpdateDTO.passed,
      gender: this.studentUpdateDTO.gender,
      countryId: this.studentUpdateDTO.countryId,
      subjects: this.studentUpdateDTO.subjects
    });

    // push all subject to form array
    this.studentUpdateDTO.subjects.forEach(
      (s) => {
        var newFormGroup = this.formBuilder.group({
          subjectId: [s.subjectId],
          subjectName: [s.subjectName, [Validators.required]],
          mark: [s.mark, [Validators.required]],
          studentId: s.studentId
        });

        this.formSubjectsArr.push(newFormGroup);
      });

  }


  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }


}
