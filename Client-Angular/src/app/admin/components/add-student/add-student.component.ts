import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { Country } from 'src/app/admin/models/country';
import { StudentCreateDTO } from 'src/app/admin/models/StudentCreateDTO';
import { NgForm, ValidationErrors } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { StudentsService } from '../../services/students.service';
import { ICanDeactivate } from '../../guards/can-deactivate-guard.service';
import { CountriesService } from '../../services/countries.service';
import { Student } from '../../models/Student';

/*
  Template Driven Form
*/


@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit, OnDestroy, ICanDeactivate {

  @ViewChild('newStudentForm', { static: true }) newStudentForm: NgForm | any; // to access the form
  studentCreateDTO: StudentCreateDTO;
  countries!: Observable<Country[]>;
  currYear: number;
  genders: string[];     // for dynamic radio buttons
  subscriptions: Subscription[];
  canLeave: boolean;


  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private location: Location,
    private router: Router
  ) {
    this.newStudentForm = null;
    this.studentCreateDTO = new StudentCreateDTO();
    this.currYear = new Date().getFullYear();
    this.genders = ['Female', 'Male', 'Other'];
    this.subscriptions = [];
    this.canLeave = true;
  }


  ngOnInit(): void {

    // get, set list using async pipe bcoz no processing needed.
    this.countries = this.countriesService.getCountries();

    this.subscriptions.push(
      this.newStudentForm.form.valueChanges.subscribe(
        (value: any) => {
          // console.log(value);
          this.canLeave = false;
        }
      )
    );

  }


  onSubmitClick() {
    if (this.newStudentForm.valid) {
      this.subscriptions.push(
        this.studentsService.addStudent(this.studentCreateDTO).subscribe(
          (r: Student) => {
          },
          (e) => {
            console.log(e);
          }
        )
      );

      // better alternaive is router below
      // this.location.back();

      // .navigate is preferred to .navigateByUrl
      this.router.navigate(['/admin', 'studentscrud']); // ['/parent', 'child']
    }
  }


  onResetClick() {
    this.newStudentForm.resetForm();
  }


  ngOnDestroy(): void {
    this.subscriptions.forEach(item => {
      item.unsubscribe();
    });
  }

}
