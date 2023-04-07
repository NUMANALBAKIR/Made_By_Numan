import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';

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


  // child => this service => grandchild2
  turnBlue() {

    this.color = 'blue';

    this.obseversGrandChilds.forEach(observer => {
      observer.next(this.color); // notify change to grandchild2
    });
  }


}
