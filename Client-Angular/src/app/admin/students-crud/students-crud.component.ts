import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CountriesService } from 'src/app/countries.service';
import { Country } from 'src/app/country';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';
import { SubjectsList } from 'src/app/subjects-list';
import { SubjectsListsService } from 'src/app/subjects-lists.service';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private countriesService: CountriesService,
    private subjectsListsService: SubjectsListsService,
    private router: Router) {
  }

  i: number = 0;
  searchBy: string = 'name';
  searchText: string = '';
  students: Student[] = [];
  countries: Country[] = [];
  subjectsLists: SubjectsList[] = [];
  showSpinner: boolean= true; 

  ngOnInit(): void {
    // get and set list of students
    this.studentsService.getStudents().subscribe(
      (response: Student[]) => {
        this.students = response;
        this.showSpinner= false;
      },
      (error) => {
        console.log(error);
      }
    );

    // get and set list of subjectslist
    this.subjectsListsService.getSubjectsLists().subscribe(
      (response: SubjectsList[]) => {
        this.subjectsLists = response;
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
