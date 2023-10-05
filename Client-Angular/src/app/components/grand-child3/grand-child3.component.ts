import { Component, DoCheck } from '@angular/core';

@Component({
  selector: 'app-grand-child3',
  templateUrl: './grand-child3.component.html',
  styleUrls: ['./grand-child3.component.css']
})
export class GrandChild3Component implements DoCheck {

  constructor() {
    this.grandChild3Color = 'white';
    this.itsColor = 'white';
   }


  grandChild3Color: string;
  itsColor: string;     // common in both gc3, gc4. used in a foreach


  // on every change detection
  ngDoCheck(): void {
    this.itsColor = this.grandChild3Color;
  }


}
