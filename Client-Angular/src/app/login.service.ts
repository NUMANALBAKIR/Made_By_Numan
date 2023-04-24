import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, pipe } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from './user';

// under construction

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }

  username: any = null;
  private _apiUrl: string = 'https://localhost:5001/api/StudentsAPI';
  user: any = null;

  public Login(user: User): Observable<any> {
    return this.httpClient.post<any>(this._apiUrl, user, { responseType: 'json' })
      .pipe(map(
        (user) => {
          if (user) {
            this.username = user.username;
            this.user= user;
          }
          return user;
        }
      ));
  }
  
  public Logout(){
    this.username=null;
  }
  
  
}
