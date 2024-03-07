import { EventEmitter, Inject, Injectable } from '@angular/core';
import {  BehaviorSubject, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  public MONTHS_BS = new BehaviorSubject<any[]>([]);
  public Months_Emitter = new EventEmitter<any[]>();
  BaseURL: string = '';

  constructor(@Inject('BASE_URL') baseUrl: string) {
    if (baseUrl === 'http://localhost:4200/') {
      this.BaseURL = 'https://localhost:44301/api/Students/';
    }
    else {
      this.BaseURL = baseUrl + 'api/Students/';
    }
  }

  emitMonths(data: any[]){
     this.Months_Emitter.next(data);
  }

  getMonthsEmitter(){
    return this.Months_Emitter;
  }

  getMonths() {
    const ms = [
      { Name: 'Jan', Value: '1' },
      { Name: 'Feb', Value: '2' },
      { Name: 'Mar', Value: '3' },
    ];
    return of(ms);
  }

}
