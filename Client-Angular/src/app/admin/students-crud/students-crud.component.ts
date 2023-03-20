import { Component, OnInit } from '@angular/core';
import { StudentsService } from 'src/app/students.service';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  constructor(private studentsService: StudentsService){
  }

  students: any[] = []; //instead of Student[]...just for learning

  ngOnInit(): void {
    this.students= this.studentsService.getStudents();

  }

  onDetailsClick($event: any){
    console.log($event.target.innerHTML);
    $event.target.remove();
  }

}
