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

  // receive emitter upon child button click 
  parentsMethod(event: any) {
    this.parentColorValue = event.parentColor;
  }

}
