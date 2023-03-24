import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  constructor(
    private studentsService: StudentsService,
    private location: Location
  ) { }

  studentToEdit: Student = new Student();
  studentId: number = this.studentsService.studentIdPassed;

  ngOnInit(): void {
    // set student info by id
    this.studentsService.getStudent(this.studentId).subscribe(
      (response: Student) => { this.studentToEdit = response },
      (error) => { console.log(error) }
    );
  }

  onConfirmClick(){
    this.studentsService.editStudent(this.studentToEdit).subscribe(
      (r) => {
        
       },
      (e) => { console.log(e) }
    );

    this.location.back();    
  }

}
