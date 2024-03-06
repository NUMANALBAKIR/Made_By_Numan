import { Injectable } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  MONTHS_BS = new BehaviorSubject<any[]>([]);

  constructor() { }

  getMonths() {
    const ms = [
      { Name: 'Jan', Value: '1' },
      { Name: 'Feb', Value: '2' },
      { Name: 'Mar', Value: '3' },
    ];
    return of(ms);
  }

}
