import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Student } from '../models/Student';
import { StudentCreateDTO } from '../models/StudentCreateDTO';
import { StudentUpdateDTO } from '../models/StudentUpdateDTO';
import { StudentDTO } from '../models/StudentDTO';
import { ApiPaths, environment } from 'src/environments/environment';

@Injectable(
  { providedIn: 'root' }
)
export class StudentsService {

  private url: string;
  public studentIdPassed: number;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.baseUrl}/${ApiPaths.Students}`;
    this.studentIdPassed = 0;
  }


  getStudent(studentId: number): Observable<StudentDTO> {
    let fullUrl = `${this.url}/${studentId}`;
    return this.httpClient.get<StudentDTO>(fullUrl, { responseType: 'json' })
      .pipe(map(
        (data: StudentDTO | any) => {
          return data;
        }
      ));
  }


  getStudents(): Observable<StudentDTO[]> {
    return this.httpClient.get<StudentDTO[]>(this.url, { responseType: 'json' })
      .pipe(map(
        (data: StudentDTO[]) => {
          for (let i = 0; i < data.length; i++) {
            data[i].name = data[i].name.toUpperCase();
          }
          return data;  // don't forget return
        }
      ));
  }


  addStudent(studentToCreate: StudentCreateDTO): Observable<StudentDTO> {
    this.url = 'https://localhost:5003/api/StudentsAPI';
    return this.httpClient.post<StudentDTO>(this.url, studentToCreate, { responseType: 'json' });
  }


  updateStudent(studentToUpdate: StudentUpdateDTO): Observable<Student> {
    this.url = 'https://localhost:5003/api/StudentsAPI/' + studentToUpdate.studentId;
    return this.httpClient.put<StudentDTO>(this.url, studentToUpdate, { responseType: 'json' });
  }


  deleteStudent(studentId: number): Observable<string> {
    return this.httpClient.delete<string>(this.url + '/' + studentId, { responseType: 'json' });
  }


  searchStudents(searchBy: string, searchText: string): Observable<StudentDTO[]> {
    let fullUrl = `${this.url}/${searchBy}/${searchText}`;
    return this.httpClient.get<StudentDTO[]>(fullUrl, { responseType: 'json' });
  }

}
