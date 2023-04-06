import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-parent',
  templateUrl: './parent.component.html',
  styleUrls: ['./parent.component.css']
})
export class ParentComponent implements OnInit {

  constructor() { }

  parentColorValue: string = '';
  toChildColorValue: string = '';

  ngOnInit(): void {
  }

  parentsMethod(event: any) {
    debugger;
    this.parentColorValue = event.parentColor;
  }

}
