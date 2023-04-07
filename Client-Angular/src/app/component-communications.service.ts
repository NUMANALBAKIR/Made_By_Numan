import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponentCommunicationsService {

  constructor() { }

  public color: string = '';
  //subscribers //note: [] is after <>
  private obseversGrandChilds: Observer<string>[] = [];

  public observableChild: Observable<string> = new Observable(
    (observer: Observer<string>) => {
      this.obseversGrandChilds.push(observer);
    }
  );


  // child => this service => grandchild2
  turnBlue() {

    this.color = 'blue';

    this.obseversGrandChilds.forEach(element => {
      element.next(this.color);
    });
  }


}
