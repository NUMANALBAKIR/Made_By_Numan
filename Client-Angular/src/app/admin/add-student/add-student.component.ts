import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';
import { Student } from 'src/app/Student';
import { StudentCreateDTO } from 'src/app/Models/StudentCreateDTO';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';

/*
Template Driven Form
*/


@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {


  @ViewChild('newStudentForm') newStudentForm: NgForm | any; // to access the form
  studentCreateDTO: StudentCreateDTO;
  countries!: Observable<Country[]>;
  currYear: number;
  genders: string[];     // for dynamic radio buttons


  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private location: Location
  ) {
    this.newStudentForm = null;
    this.studentCreateDTO = new StudentCreateDTO();
    this.currYear = new Date().getFullYear();
    this.genders = ['Female', 'Male', 'Other'];
  }


  ngOnInit(): void {

    // get, set list using async pipe bcoz no processing needed.
    this.countries = this.countriesService.getCountries();
  }

  onSubmitClick() {
    
    // debugger;

    if (this.newStudentForm.valid) {
      this.studentsService.addStudent(this.studentCreateDTO).subscribe(
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
    this.newStudentForm.resetForm();
  }

}
