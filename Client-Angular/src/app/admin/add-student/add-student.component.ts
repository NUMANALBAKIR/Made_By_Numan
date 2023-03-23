import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private location: Location
  ) { }

  newStudent: Student = new Student();

  ngOnInit(): void {

  }

  onConfirmClick() {
    this.studentsService.insertStudent(this.newStudent).subscribe(
      (r) => { },
      (e) => { console.log(e) }
    );
    
    this.location.back();    
  }

}
