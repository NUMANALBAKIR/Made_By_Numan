import { Injectable } from '@angular/core';

@Injectable()
export class StudentsService {


  getStudents(): any[] {

    const students = [
      {
        ID: 1,
        Name: "Sam",
        DateOfBirth: "1-1-2000",
        Age: 23,
        Pass: 'Passed',
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

    return students;
  }


}
