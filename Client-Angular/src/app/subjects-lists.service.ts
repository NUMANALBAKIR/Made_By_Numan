import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SubjectsList } from './subjects-list';


@Injectable({
  providedIn: 'root'
})
export class SubjectsListsService {

  private _apiUrl: string = 'http://localhost:5091/api/SubjectsListsAPI';

  constructor(private httpClient: HttpClient) {
  }

  getSubjectsList(subjectsListId: number): Observable<SubjectsList> {
    return this.httpClient.get<SubjectsList>(this._apiUrl + '/' + subjectsListId, { responseType: 'json' });
  }


  getSubjectsLists(): Observable<SubjectsList[]> {
    return this.httpClient.get<SubjectsList[]>(this._apiUrl, { responseType: 'json' })
      .pipe(map(
        (data: SubjectsList[]) => {
          
          return data;
        }
      ));
  }


  addSubjectsList(subjectsListToAdd: SubjectsList): Observable<SubjectsList> {
    return this.httpClient.post<SubjectsList>(this._apiUrl, subjectsListToAdd, { responseType: 'json' });
  }

  editSubjectsList(subjectsListToEdit: SubjectsList): Observable<SubjectsList> {
    return this.httpClient.put<SubjectsList>(this._apiUrl + '/' + subjectsListToEdit.subjectsListId, subjectsListToEdit, { responseType: 'json' });
  }

  deleteSubjectsList(subjectsListId: number): Observable<string> {
    return this.httpClient.delete<string>(this._apiUrl + '/' + subjectsListId, { responseType: 'json' });
  }

  searchSubjectsLists(searchBy: string, searchText: string): Observable<SubjectsList[]> {
    let url = this._apiUrl + '/' + searchBy + '/' + searchText;
    return this.httpClient.get<SubjectsList[]>(url, { responseType: 'json' });
  }
}
