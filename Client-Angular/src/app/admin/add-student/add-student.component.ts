import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { StudentsCRUDComponent } from '../students-crud/students-crud.component';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private location: Location,
    // private studentsCRUDComponent: StudentsCRUDComponent
  ) { }

  newStudent: Student = new Student();

  ngOnInit(): void {
  }

  onAddClick() {
    this.studentsService.insertStudent(this.newStudent).subscribe(
      (r) => { },
      (e) => { console.log(e) }
    );
    this.location.back();
  }

}
