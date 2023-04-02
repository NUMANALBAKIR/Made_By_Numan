import { Component, OnInit, ViewChild } from '@angular/core';
import { Student } from 'src/app/Student';
import { StudentsService } from 'src/app/students.service';
import { Location } from '@angular/common';
import { StudentUpdateDTO } from 'src/app/Models/StudentUpdateDTO';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  @ViewChild('editForm') editForm: NgForm|any= null;
  

  constructor(
    private studentsService: StudentsService,
    private location: Location
  ) { }

  studentToEdit: StudentUpdateDTO = new StudentUpdateDTO();
  studentId: number = this.studentsService.studentIdPassed;

  ngOnInit(): void {

    // set student info by id
    setTimeout(() => {
      this.studentsService.getStudent(this.studentId).subscribe(
        (response: Student) => {
          this.studentToEdit = response;
        },
        (error) => {
          console.log(error);
        }
      );
    }, 200);
  }


  onConfirmClick() {

    if (this.editForm.valid) {
      this.studentsService.editStudent(this.studentToEdit).subscribe(
        (r: Student) => {
        },
        (e) => {
          console.log(e);
        }
      );
  
      this.location.back();      
    }

  }

}
