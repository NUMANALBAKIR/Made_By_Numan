import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';

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

  students: Student[] = [];
  i: number = 0;
  searchBy: string = 'name';
  searchText: string = '';


  ngOnInit(): void {
    // get and set list of students
    this.studentsService.getStudents().subscribe(
      (response: Student[]) => {
        this.students = response;
      },
      (error) => {
        console.log(error);
      }
    );

    debugger;


  }

  onDetailsClick(event: any, studentId: number) {
    console.log(event.target.innerHTML);
    event.target.remove();
  }

  onEditClick(event: any, studentId: number) {
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
    //needed
    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
    this.router.navigateByUrl('/studentscrud');
  }

  onSearchClick() {
    return this.studentsService.searchStudents(this.searchBy, this.searchText).subscribe(
      (r: Student[]) => {
        this.students = r;
      },
      (e: any) => {
        console.log(e);
      }
    );
  }





}
