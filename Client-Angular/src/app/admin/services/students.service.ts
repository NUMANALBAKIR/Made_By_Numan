import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Student } from '../models/Student';
import { StudentCreateDTO } from '../models/StudentCreateDTO';
import { StudentUpdateDTO } from '../models/StudentUpdateDTO';
import { StudentDTO } from '../models/StudentDTO';

@Injectable(
  { providedIn: 'root' }
)
export class StudentsService {

  private _apiUrl: string;
  studentIdPassed: number;


  constructor(private httpClient: HttpClient) {
    this._apiUrl = 'http://localhost:5091/api/StudentsAPI';
    this.studentIdPassed = 0;
  }


  getStudent(studentId: number): Observable<StudentDTO> {
    return this.httpClient.get<StudentDTO>(this._apiUrl + '/' + studentId, { responseType: 'json' })
      .pipe(map(
        (data: StudentDTO | any) => {
          return data;
        }
      ));
  }


  getStudents(): Observable<StudentDTO[]> {
    return this.httpClient.get<StudentDTO[]>(this._apiUrl, { responseType: 'json' })
      .pipe(map(
        (data: StudentDTO[]) => {
          for (let i = 0; i < data.length; i++) {
            data[i].name = data[i].name.toUpperCase();
          }
          return data;
        }
      ));
  }


  addStudent(studentToCreate: StudentCreateDTO): Observable<StudentDTO> {
    return this.httpClient.post<StudentDTO>(this._apiUrl, studentToCreate, { responseType: 'json' });
  }


  updateStudent(studentToUpdate: StudentUpdateDTO): Observable<Student> {
    return this.httpClient.put<StudentDTO>(this._apiUrl + '/' + studentToUpdate.studentId, studentToUpdate, { responseType: 'json' });
  }


  deleteStudent(studentId: number): Observable<string> {
    return this.httpClient.delete<string>(this._apiUrl + '/' + studentId, { responseType: 'json' });
  }


  searchStudents(searchBy: string, searchText: string): Observable<StudentDTO[]> {
    let url = this._apiUrl + '/' + searchBy + '/' + searchText;
    return this.httpClient.get<StudentDTO[]>(url, { responseType: 'json' });
  }





}