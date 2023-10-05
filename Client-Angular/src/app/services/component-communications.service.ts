import { Injectable } from '@angular/core';
import { Observable, Observer, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponentCommunicationsService {

  constructor() { }
  public color: string = '';

  // subscribers/observers (just an array) // note: [] is after <>
  private observeRsArr: Observer<string>[] = [];  // big R for easy reading

  public observable = new Observable<string>(

    // everytime something new subscribes (is observer) to this 'observable', is added to observeRsArr
    (observer: Observer<string>) => {
      this.observeRsArr.push(observer);
    }
  );

  // parent => this service => grandchildren 1+2
  subject = new Subject<string>();


  // using observable
  // parent => this service => grandchild 1
  turnYellow() {
    if (this.color != 'yellow') {
      this.color = 'yellow';
    }
    else {
      this.color = 'white';
    }

    this.observeRsArr.forEach(
      observer => {
        observer.next(this.color); // notify change to observer grandchild 1
      }
    );
  }

  // using subject
  // parent => this service => grandchildren 1+2
  turnRed() {
    if (this.color != 'red') {
      this.color = 'red';
    }
    else {
      this.color = 'white';
    }
    this.subject.next(this.color);
  }

}
