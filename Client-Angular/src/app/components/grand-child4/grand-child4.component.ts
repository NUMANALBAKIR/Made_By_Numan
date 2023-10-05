import { Component, DoCheck } from '@angular/core';

@Component({
  selector: 'app-grand-child4',
  templateUrl: './grand-child4.component.html',
  styleUrls: ['./grand-child4.component.css']
})
export class GrandChild4Component implements DoCheck {

  constructor() {
    this.grandChild4Color = 'white';
    this.itsColor = 'white';
  }


  grandChild4Color: string;
  itsColor: string;     // common in both gc3, gc4. used in a foreach


  // on every change detection
  ngDoCheck(): void {
    this.itsColor = this.grandChild4Color;
  }



}
