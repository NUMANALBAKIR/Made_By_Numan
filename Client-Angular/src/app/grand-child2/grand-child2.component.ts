import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-grand-child2',
  templateUrl: './grand-child2.component.html',
  styleUrls: ['./grand-child2.component.css']
})
export class GrandChild2Component implements OnInit {

  constructor() { }

  grandChildColorValue: string = '#ffc0cb';


  ngOnInit(): void {
  }

}
