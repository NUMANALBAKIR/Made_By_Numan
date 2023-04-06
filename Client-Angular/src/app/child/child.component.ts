import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnInit {

  constructor() { }

  @Input('childColorValue') childColorValue: string = '';
  colorForGrandChild: string = '';
  @Output('clickInChild') clickInChild = new EventEmitter<any[]>();



  ngOnInit(): void {
  }



  onClickInChild(event: any) 
  {
    this.clickInChild.emit(event );
  }  


}
