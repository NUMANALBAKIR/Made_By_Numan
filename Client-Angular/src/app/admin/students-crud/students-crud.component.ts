import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Country } from 'src/app/country';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import * as $ from 'jquery';
import { StudentDTO } from 'src/app/Models/StudentDTO';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  i: number;
  searchBy: string;
  searchText: string;
  students: StudentDTO[];
  countries: Country[];
  showSpinner: boolean;


  constructor(private studentsService: StudentsService, private router: Router) {
    this.i = 0;
    this.searchBy = 'name';
    this.searchText = '';
    this.students = [];
    this.countries = [];
    this.showSpinner = true;
  }


  ngOnInit(): void {
    // get and set list of students
    this.studentsService.getStudents().subscribe(
      (response: StudentDTO[]) => {
        this.students = response;
        this.showSpinner = false;
      },
      (error) => {
        console.log(error);
      }
      );
      
      // debugger;
  }


  onDetailsClick(event: any, studentId: number) {
    console.log(event.target.innerHTML);
    event.target.remove();
  }


  onUpdateClick(event: any, studentId: number) {
    this.studentsService.studentIdPassed = studentId;
  }

  onDeleteClick(event: any, i: number) {
    this.i = i;
  }

  onConfirmDelete() {
    this.studentsService.deleteStudent(this.students[this.i].studentId).subscribe(
      (r: string) => {
      },
      (e: any) => {
        console.log(e);
      }
    );

    //  needed
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
    this.router.navigateByUrl('/studentscrud');

  }

  // button commented out
  // onSearchClick() {
  //   return this.studentsService.searchStudents(this.searchBy, this.searchText).subscribe(
  //     (r: StudentDTO[]) => {
  //       this.students = r;
  //     },
  //     (e: any) => {
  //       console.log(e);
  //     }
  //   );
  // }


  onCancelClick() {
    $("#crossIcon").trigger("click");
  }



}
