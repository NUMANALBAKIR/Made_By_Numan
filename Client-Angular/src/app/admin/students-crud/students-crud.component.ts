import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  constructor(private studentsService: StudentsService) {
  }

  students: Student[] = [];

  ngOnInit(): void {
    this.studentsService.getStudents().subscribe(
      (response: Student[]) => { this.students = response },
      (error) => { console.log(error) }
    );

  }

  onDetailsClick($event: any) {
    console.log($event.target.innerHTML);
    $event.target.remove();
  }

}
