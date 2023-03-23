import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from './student';

@Injectable(
  { providedIn: 'root' }
)
export class StudentsService {

  private _apiUrl: string = 'https://localhost:5001';
  public studentIdPassed: number = 0;

  constructor(private httpClient: HttpClient) {
  }


  // getStudents(): any[] {

  //   const students = [
  //     {
  //       ID: 1,
  //       Name: "Sam",
  //       DateOfBirth: "1-1-2000",
  //       Age: 23,
  //       Pass: 'Passed',
  //       Subjects: [
  //         {
  //           Name: 'English',
  //           Grade: 80
  //         },
  //         {
  //           Name: 'Math',
  //           Grade: 60
  //         }
  //       ]
  //     },
  //     {
  //       ID: 2,
  //       Name: "Iram",
  //       DateOfBirth: "2-2-2002",
  //       Age: 24,
  //       Pass: 'failed',
  //       Subjects: [
  //         {
  //           Name: 'English',
  //           Grade: 80
  //         },
  //         {
  //           Name: 'Math',
  //           Grade: 60
  //         },
  //         {
  //           Name: 'Biology',
  //           Grade: 95
  //         },
  //       ]
  //     },
  //     {
  //       ID: 3,
  //       Name: "Zina",
  //       DateOfBirth: "3-3-2003",
  //       Age: 25,
  //       Pass: 'passed',
  //       Subjects: [
  //         {
  //           Name: 'English',
  //           Grade: 70
  //         },
  //         {
  //           Name: 'Math',
  //           Grade: 80
  //         }
  //       ]
  //     }
  //   ];

  //   return students;
  // }

  getStudent(studentId: number): Observable<Student> {
    return this.httpClient.get<Student>(this._apiUrl + '/api/StudentsAPI/' + studentId, { responseType: 'json' });
  }

  getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this._apiUrl + '/api/StudentsAPI', { responseType: 'json' });
  }

  insertStudent(newStudent: Student): Observable<Student> {
    return this.httpClient.post<Student>(this._apiUrl + '/api/StudentsAPI', newStudent, { responseType: 'json' });
  }

  editStudent(editStudent: Student): Observable<Student> {
    return this.httpClient.put<Student>(this._apiUrl + '/api/StudentsAPI/' + editStudent.studentId, editStudent, { responseType: 'json' });
  }

}
