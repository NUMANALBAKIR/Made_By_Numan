import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Student } from './Student';
import { StudentCreateDTO } from './Models/StudentCreateDTO';
import { StudentUpdateDTO } from './Models/StudentUpdateDTO';

@Injectable(
  { providedIn: 'root' }
)
export class StudentsService {

  private _apiUrl: string = 'http://localhost:5091/api/StudentsAPI';
  public studentIdPassed: number = 0;

  constructor(private httpClient: HttpClient) {
  }


  getStudent(studentId: number): Observable<Student> {
    return this.httpClient.get<Student>(this._apiUrl + '/' + studentId, { responseType: 'json' })
    .pipe(map(
      (data: Student)=> {
        // debugger;
        return data;
      }
    ));
  }


  getStudents(): Observable<Student[]> {
    return this.httpClient.get<Student[]>(this._apiUrl, { responseType: 'json' })
      .pipe(map(
        (data: Student[]) => {
          
          for (let i = 0; i < data.length; i++) {
            data[i].name = data[i].name.toUpperCase();
          }

          // debugger;
          return data;
        }
      ));
  }


  addStudent(studentToAdd: StudentCreateDTO): Observable<Student> {
    return this.httpClient.post<Student>(this._apiUrl, studentToAdd, { responseType: 'json' });
  }

  editStudent(studentToEdit: StudentUpdateDTO): Observable<Student> {
    return this.httpClient.put<Student>(this._apiUrl + '/' + studentToEdit.studentId, studentToEdit, { responseType: 'json' });
  }

  deleteStudent(studentId: number): Observable<string> {
    return this.httpClient.delete<string>(this._apiUrl + '/' + studentId, { responseType: 'json' });
  }

  searchStudents(searchBy: string, searchText: string): Observable<Student[]> {
    let url = this._apiUrl + '/' + searchBy + '/' + searchText;
    // debugger;
    return this.httpClient.get<Student[]>(url, { responseType: 'json' });
  }



}
