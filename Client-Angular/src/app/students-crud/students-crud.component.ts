import { Component, OnInit } from '@angular/core';

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
        Name: "Sam",
        DateOfBirth: "1-1-2000",
        Age: 23,
        Pass: 'passed',
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
        Name: "Iram",
        DateOfBirth: "2-2-2002",
        Age: 24,
        Pass: 'failed',
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
        Name: "Zina",
        DateOfBirth: "3-3-2003",
        Age: 25,
        Pass: 'passed',
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

  onButtonClick($event: any){
    console.log($event.target.innerHTML);
    $event.target.innerHTML= 'Just checking';
  }

}
