import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-grand-child',
  templateUrl: './grand-child.component.html',
  styleUrls: ['./grand-child.component.css']
})
export class GrandChildComponent implements OnInit {

  constructor() { }

  
  grandChildColor : string = 'white';
  white = true;


  ngOnInit(): void {
  }


  toggleBlackWhite() {
    if (this.white) {
      this.grandChildColor = 'black';
      this.white = !this.white;
    } else {
      this.grandChildColor = 'white';
      this.white = !this.white;
    }
  }

}
