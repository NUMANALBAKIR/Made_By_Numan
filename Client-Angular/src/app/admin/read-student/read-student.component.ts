import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { StudentDTO } from 'src/app/Models/StudentDTO';
import { StudentsService } from 'src/app/students.service';

@Component({
  selector: 'app-read-student',
  templateUrl: './read-student.component.html',
  styleUrls: ['./read-student.component.css']
})
export class ReadStudentComponent implements OnInit, OnDestroy {

  constructor(private activatedRoute: ActivatedRoute, private studentsService: StudentsService) {
    this.studentId = 0;
    this.subscriptionActivatedRoute = new Subscription();
  }


  studentId: number;
  subscriptionActivatedRoute: Subscription;
  studentDTO!: Observable<StudentDTO>;

  
  ngOnInit(): void {
    this.subscriptionActivatedRoute = this.activatedRoute.params.subscribe(
      (routeParams) => {
        this.studentId = Number(routeParams['studentId']);
      }
    );

    this.studentDTO = this.studentsService.getStudent(this.studentId);
  }


  ngOnDestroy(): void {
    this.subscriptionActivatedRoute.unsubscribe();
  }


}
