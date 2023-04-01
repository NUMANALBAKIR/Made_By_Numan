import { Component, OnInit } from '@angular/core';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { Country } from 'src/app/country';
import { CountriesService } from 'src/app/countries.service';
import { Student } from 'src/app/Student';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private location: Location
  ) { }

  studentToAdd: Student = new Student();
  countries: Country[] = [];


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
    this.studentsService.addStudent(this.studentToAdd).subscribe(
      (r: Student) => {
      },
      (e) => { 
        console.log(e);
       }
    );
  
    this.location.back();    
  }

}
