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

  studentToAdd: Student = new Student();

  ngOnInit(): void {

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
