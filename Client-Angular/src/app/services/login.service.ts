import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) {
    this._apiUrl = 'https://localhost:5001/api/StudentsAPI';
    this.username = null;
    this.user = null;
  }

  username: any;
  private _apiUrl: string;
  user: any;

  public Login(user: User): Observable<any> {
    return this.httpClient.post<any>(this._apiUrl, user, { responseType: 'json' })
      .pipe(map(
        (user) => {
          if (user) {
            this.username = user.username;
            this.user = user;
          }
          return user;
        }
      ));
  }

  public Logout() {
    this.username = null;
  }


}
