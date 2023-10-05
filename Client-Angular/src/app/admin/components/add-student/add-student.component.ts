import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Country } from 'src/app/admin/models/country';
import { StudentCreateDTO } from 'src/app/admin/models/StudentCreateDTO';
import { NgForm } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { StudentsService } from '../../services/students.service';
import { ICanDeactivate } from '../../guards/can-deactivate-guard.service';
import { CountriesService } from '../../services/countries.service';
import { StudentDTO } from '../../models/StudentDTO';

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
  countries!: Observable<Country[]>;  // using async pipe, to simplify subscribing to observable.
  // and becaause no manipulation is needed on the received data
  // type is observable bcoz directly receiving it from service.
  currYear: number;
  genders: string[];     // for dynamic radio buttons
  subscriptions: Subscription[];
  canLeave: boolean;


  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private router: Router
  ) {
    this.newStudentForm = null;
    this.studentCreateDTO = new StudentCreateDTO();
    this.currYear = new Date().getFullYear();
    this.genders = ['Female', 'Male', 'Other'];
    this.subscriptions = [];  // note
    this.canLeave = true;
  }


  ngOnInit(): void {

    // get, set list using async pipe bcoz no processing needed.
    this.countries = this.countriesService.getCountries();

    // handle leaving page
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
        this.studentsService.addStudent(this.studentCreateDTO).subscribe( // subscribing to the service's observable
          (r: StudentDTO) => {
          },
          (e) => {
            console.log(e);
          }
        )
      );

      this.canLeave = true;

      // this.location.back(); // of ctor's 'private location: Location'. // better alternaive is router below

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
