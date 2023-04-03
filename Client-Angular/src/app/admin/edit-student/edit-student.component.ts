import { Component, OnInit, ViewChild } from '@angular/core';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { StudentUpdateDTO } from 'src/app/Models/StudentUpdateDTO';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';

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


  // @ViewChild('editForm') editForm: NgForm | any = null;
  studentUpdateDTO: StudentUpdateDTO = new StudentUpdateDTO();
  // studentId: number = this.studentsService.studentIdPassed;
  studentId: number = 1;
  countries: Country[] = [];
  genders: string[] = ['Female', 'Male', 'Other'];  //for dynamic radio buttons

  updateFormReactve: FormGroup = new FormGroup({
    studentId: new FormControl(this.studentUpdateDTO.studentId),
    name: new FormControl(),
    dateOfBirth: new FormControl(),
    passed: new FormControl(),
    gender: new FormControl(),
    countryId: new FormControl()
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

    // populate form


  }


  onConfirmClick() {

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
    // this.updateFormReactve.resetForm();
    this.populate();
  }


  populate(): void {
    this.updateFormReactve.setValue({
      studentId: this.studentUpdateDTO.studentId,
      name: this.studentUpdateDTO.name,
      dateOfBirth: this.studentUpdateDTO.dateOfBirth,
      passed: this.studentUpdateDTO.passed,
      gender: this.studentUpdateDTO.gender,
      countryId: this.studentUpdateDTO.countryId
    });

  }

}
