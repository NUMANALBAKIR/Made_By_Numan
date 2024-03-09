import { EventEmitter, Inject, Injectable } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';

export interface Lookup {
  Name: string;
  Value: string;
}

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  public MONTHS_BS = new BehaviorSubject<any[]>([]);
  private Months_Emitter = new EventEmitter<any[]>();
  BaseURL: string = '';
  dataStore: {data : Lookup[]} = { data: [] } //note

  constructor(@Inject('BASE_URL') baseUrl: string) {
    if (baseUrl === 'http://localhost:4200/') {
      this.BaseURL = 'https://localhost:44301/api/Students/';
    }
    else {
      this.BaseURL = baseUrl + 'api/Students/';
    }
  }

  emitMonths(data: any[]) {
    this.Months_Emitter.next(data);
  }

  getMonthsEmitter() {
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
