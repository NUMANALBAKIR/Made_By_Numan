import { Component, OnInit, ViewChild } from '@angular/core';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { StudentUpdateDTO } from 'src/app/Models/StudentUpdateDTO';
import { FormArray, FormControl, FormGroup, NgForm } from '@angular/forms';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';


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
    private location: Location
  ) {
  }


  // studentId: number = this.studentsService.studentIdPassed;
  studentId: number = 1;
  countries: Country[] = [];
  genders: string[] = ['Female', 'Male', 'Other'];  // for dynamic radio buttons
  studentUpdateDTO: StudentUpdateDTO = new StudentUpdateDTO();

  updateFormReactve: FormGroup = new FormGroup({
    studentId: new FormControl(this.studentUpdateDTO.studentId),
    name: new FormControl(),
    dateOfBirth: new FormControl(),
    passed: new FormControl(),
    gender: new FormControl(),
    countryId: new FormControl(),
    subjects: new FormArray([])
  });


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
      (response: Student) => {
        this.studentUpdateDTO = response;
      },
      (e) => {
        console.log(e);
      }
    );


    // wait for service responses and then populate.
    setTimeout(() => {
      this.populate();
    }, 1000);


    // valueChanges
    this.updateFormReactve.valueChanges.subscribe(
      (value: any) => {
        console.log(value);
      }
    );


  }


  get formSubjectsArr() {
    return <FormArray>this.updateFormReactve.get('subjects');
  }


  onAddClick() {
    var newFormGroup = new FormGroup({
      subjectName: new FormControl(),
      marks: new FormControl()
    });

    this.formSubjectsArr.push(newFormGroup);

  }

  onRemoveClick(i: number){
    this.formSubjectsArr.removeAt(i);
  }


  onSubmitClick() {

    if (this.updateFormReactve.valid) {
      this.studentsService.editStudent(this.studentUpdateDTO).subscribe(
        (r: Student) => {
        },
        (e) => {
          console.log(e);
        }
      );

      this.location.back();
    }

  }


  onResetClick() {

    // this.updateFormReactve.reset();

    // patchValue
    // this.updateFormReactve.patchValue({
    //   studentId: this.studentId
    // });

    // similar to above
    this.updateFormReactve.reset({
      studentId: this.studentId
    });
  }


  // populate form
  populate(): void {
    this.updateFormReactve.patchValue({
      studentId: this.studentUpdateDTO.studentId,
      name: this.studentUpdateDTO.name,
      dateOfBirth: this.studentUpdateDTO.dateOfBirth,
      passed: this.studentUpdateDTO.passed,
      gender: this.studentUpdateDTO.gender,
      countryId: this.studentUpdateDTO.countryId,
      subjects: []
    });
  }

}
