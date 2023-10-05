import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Country } from 'src/app/admin/models/country';
import { StudentsService } from 'src/app/admin/services/students.service';
import * as $ from 'jquery';
import { StudentDTO } from 'src/app/admin/models/StudentDTO';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit, OnDestroy {

  i: number;
  searchBy: string;
  searchText: string;
  students: StudentDTO[];
  countries: Country[];
  showSpinner: boolean;
  subscriptions: Subscription[];


  constructor(private studentsService: StudentsService, private router: Router) {
    this.i = 0;
    this.searchBy = 'name';
    this.searchText = '';
    this.students = [];
    this.countries = [];
    this.showSpinner = true;
    this.subscriptions = [];

  }


  ngOnInit(): void {
    // get and set list of students
    this.subscriptions.push(
      this.studentsService.getStudents().subscribe(
        (response: StudentDTO[]) => {
          this.students = response;
          this.showSpinner = false;
        },
        (error) => {
          console.log(error);
        })
    );
  }


  // just a note
  // onReadClick(event: any) {
  //   // console.log(event.target.innerHTML);
  //   // event.target.remove();

  //   this.router.navigateByUrl('readstudent');
  // }


  onUpdateClick(event: any, studentId: number) {
    this.studentsService.studentIdPassed = studentId;
  }

  onDeleteClick(event: any, i: number) {
    this.i = i;
  }

  onConfirmDelete() {
    this.subscriptions.push(
      this.studentsService.deleteStudent(this.students[this.i].studentId).subscribe(
        (r: string) => {
        },
        (e: any) => {
          console.log(e);
        })
    );

    window.location.reload();
  }


  onCancelClick() {
    $("#crossIcon").trigger("click");
  }


  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }


}
