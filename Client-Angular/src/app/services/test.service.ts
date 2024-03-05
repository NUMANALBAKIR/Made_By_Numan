import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TestService {

  BaseURL: string = '';

  constructor(@Inject('BASE_URL') baseUrl: string) {
    if (baseUrl === 'http://localhost:4200/') {
      this.BaseURL = 'https://localhost:44301/api/Students/';
    }
    else {
      this.BaseURL = baseUrl + 'api/Students/';
    }
  }

}
