import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';
import { Student } from 'src/app/Student';
import { StudentCreateDTO } from 'src/app/Models/StudentCreateDTO';
import { NgForm } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';

/*
  Template Driven Form
*/


@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit, OnDestroy {

  @ViewChild('newStudentForm') newStudentForm: NgForm | any; // to access the form
  studentCreateDTO: StudentCreateDTO;
  countries!: Observable<Country[]>;
  currYear: number;
  genders: string[];     // for dynamic radio buttons
  subscription: Subscription;


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
    this.subscription = new Subscription();
  }


  ngOnInit(): void {

    // get, set list using async pipe bcoz no processing needed.
    this.countries = this.countriesService.getCountries();
  }


  onSubmitClick() {
    if (this.newStudentForm.valid) {
      this.subscription = this.studentsService.addStudent(this.studentCreateDTO).subscribe(
        (r: Student) => {

        },
        (e) => {
          console.log(e);
        }
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
    this.subscription.unsubscribe();
  }

}
