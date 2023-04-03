import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';
import { Student } from 'src/app/Student';
import { StudentCreateDTO } from 'src/app/Models/StudentCreateDTO';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  @ViewChild('newForm') newForm: NgForm | any = null;


  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private location: Location
  ) { }

  studentCreateDTO: StudentCreateDTO = new StudentCreateDTO();
  countries: Country[] = [];
  currYear: number = new Date().getFullYear(); 


  ngOnInit(): void {

    // get and set list of countries
    this.countriesService.getCountries().subscribe(
      (response: Country[]) => {
        this.countries = response;
      },
      (error) => {
        console.log(error);
      }
    );

  }

  onConfirmClick() {
    // debugger;

    if (this.newForm.valid) {
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


  onResetClick(){
    this.newForm.resetForm();
  }

}
