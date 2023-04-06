import { Component, OnInit, ViewChild } from '@angular/core';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { StudentUpdateDTO } from 'src/app/Models/StudentUpdateDTO';
import { FormArray, FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';
import { CustomValidatorsService } from 'src/app/custom-validators.service';
import { Router } from '@angular/router';
import { StudentDTO } from 'src/app/Models/StudentDTO';


/*
Reactive Form
*/

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private location: Location,
    private customValidatorsService: CustomValidatorsService,
    private formBuilder: FormBuilder,
    private router: Router


  ) {
  }


  studentId: number = 1;
  // studentId: number = this.studentsService.studentIdPassed;
  countries: Country[] = [];
  genders: string[] = ['Female', 'Male', 'Other'];  // for dynamic radio buttons
  studentUpdateDTO: StudentUpdateDTO = new StudentUpdateDTO();

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
  updateFormReactve: FormGroup = this.formBuilder.group({
    studentId: [null, [Validators.required, Validators.pattern(/^[0-9]*$/)], [this.customValidatorsService.UniqueStudentId()], { updateOn: 'blur' }],
    name: [null, [Validators.required, Validators.minLength(3), Validators.pattern(/^[A-Za-z ]+$/)]],
    dateOfBirth: [null, [Validators.required, this.customValidatorsService.minAge(7)]],
    passed: ['', [Validators.required]],
    gender: ['', [Validators.required]],
    countryId: [null, [Validators.required]],
    subjects: this.formBuilder.array([])
  },
    {
      validators: [
        this.customValidatorsService.compareOthers('gender', 'countryId')
      ]
    });

  submitted = false;


  ngOnInit() {

    // get, set countries list by id
    this.countriesService.getCountries().subscribe(
      (response: Country[]) => {
        this.countries = response;
      },
      (e) => {
        console.log(e);
      }
    );

    // get, set student info by id
    this.studentsService.getStudent(this.studentId).subscribe(
      (response: StudentDTO) => {
        this.studentUpdateDTO = response;
      },
      (e) => {
        console.log('-- From editComponent: ' + e);
      }
    );


    // wait for service responses and then populate.
    setTimeout(() => {
      this.populateForm();
    }, 500);


    // valueChanges
    this.updateFormReactve.valueChanges.subscribe(
      (value: any) => {
        // console.log(value);
        // console.log(this.updateFormReactve);
      }
    );


  }


  get formSubjectsArr() {
    return <FormArray>this.updateFormReactve.get('subjects');
  }


  onAddClick() {

    // way 1. way 2 is below
    // var newFormGroup = new FormGroup({
    //   subjectName: new FormControl(),
    //   mark: new FormControl()
    // });

    // way 2
    var newFormGroup = this.formBuilder.group({
      subjectName: [null, [Validators.required]],
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

    // if (this.updateFormReactve.valid) {     
    //   this.studentUpdateDTO = this.updateFormReactve.value as StudentUpdateDTO;
    //   console.log(this.updateFormReactve);
    // }

    if (this.updateFormReactve.valid) {

      this.studentUpdateDTO = this.updateFormReactve.value as StudentUpdateDTO;

      this.studentsService.updateStudent(this.studentUpdateDTO).subscribe(
        (r: Student) => {
        },
        (e) => {
          console.log(e);
        }
      );

      // better alternaive is router below
      // this.location.back();

      this.router.navigate(['', 'studentscrud']); // [ parent, child ]

    }

  }


  onResetClick() {

    // using 'patchValue'
    // this.updateFormReactve.patchValue({
    //   studentId: this.studentId
    // });

    // this.updateFormReactve.reset();

    // similar to aboves
    this.updateFormReactve.reset({
      passed: this.studentUpdateDTO.passed,
      gender: this.studentUpdateDTO.gender
    });
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
  }

}
