import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Country } from 'src/app/country';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import { SubjectsList } from 'src/app/subjects-list';
import * as $ from 'jquery';
import { StudentDTO } from 'src/app/Models/StudentDTO';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private router: Router) {
  }

  i: number = 0;
  searchBy: string = 'name';
  searchText: string = '';
  students: StudentDTO[] = [];
  countries: Country[] = [];
  subjectsLists: SubjectsList[] = [];
  showSpinner: boolean = true;

  ngOnInit(): void {
    // debugger;
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

  onSearchClick() {
    return this.studentsService.searchStudents(this.searchBy, this.searchText).subscribe(
      (r: StudentDTO[]) => {
        this.students = r;
      },
      (e: any) => {
        console.log(e);
      }
    );
  }


  onCancelClick() {
    $("#crossIcon").trigger("click");
  }



}
