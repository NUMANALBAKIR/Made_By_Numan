import { Component, OnInit } from '@angular/core';
import { Student } from '../Student';

@Component({
  selector: 'app-students-crud',
  templateUrl: './students-crud.component.html',
  styleUrls: ['./students-crud.component.css']
})
export class StudentsCRUDComponent implements OnInit {

  students: any[] = []; //instead of Student[], just for learning.

  ngOnInit(): void {

    this.students = [
      {
        ID: 1,
        Name: "Numan",
        DateOfBirth: "1-1-2000",
        Age: 23,
        Gender: "male",
        Class: 5,
        Subjects: [
          {
            Name: 'English',
            Grade: 80
          },
          {
            Name: 'Math',
            Grade: 60
          }
        ]
      },
      {
        ID: 2,
        Name: "st 2",
        DateOfBirth: "2-2-2002",
        Age: 24,
        Gender: "male",
        Class: 6,
        Subjects: [
          {
            Name: 'English',
            Grade: 80
          },
          {
            Name: 'Math',
            Grade: 60
          },
          {
            Name: 'Biology',
            Grade: 95
          },
        ]
      },
      {
        ID: 3,
        Name: "st 3",
        DateOfBirth: "3-3-2003",
        Age: 25,
        Gender: "female",
        Class: 7,
        Subjects: [
          {
            Name: 'English',
            Grade: 70
          },
          {
            Name: 'Math',
            Grade: 80
          }
        ]
      }
    ];
  }

}
