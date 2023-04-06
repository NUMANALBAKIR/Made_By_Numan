import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-grand-child',
  templateUrl: './grand-child.component.html',
  styleUrls: ['./grand-child.component.css']
})
export class GrandChildComponent implements OnInit {

  constructor() { }

  @Input('grandChildColorValue') grandChildColorValue : string = '';

  ngOnInit(): void {
  }

}
