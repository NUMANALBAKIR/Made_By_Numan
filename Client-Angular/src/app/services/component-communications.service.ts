import { Injectable } from '@angular/core';
import { Observable, Observer, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponentCommunicationsService {

  constructor() { }

  public color: string = '';
  // subscribers/observers (just an array) // note: [] is after <>
  private obseversGrandChilds: Observer<string>[] = [];

  // everytime something new subscribes (is observer), is added to arr of observers
  public observableChild = new Observable<string>(
    (observerGrandChild: Observer<string>) => {
      this.obseversGrandChilds.push(observerGrandChild);
    }
  );

  // parent => this service => grandchildren 1+2
  subjectParent = new Subject<string>();

  // parent => this service => grandchild 3
  turnYellow() {

    if (this.color != 'yellow') {
      this.color = 'yellow';
    }
    else {
      this.color = 'white';
    }

    this.obseversGrandChilds.forEach(observer => {
      observer.next(this.color); // notify change to grandchild2
    });
  }

  // parent => this service => grandchildren 1+2
  turnRed() {
    if (this.color != 'red') {
      this.color = 'red';
    }
    else {
      this.color = 'white';
    }
    this.subjectParent.next(this.color);
  }

}
