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
  @Output('childsClickEmitter') childsClickEmitter = new EventEmitter<any>();



  ngOnInit(): void {
  }



  onClickToParent(event: any) {
    this.childsClickEmitter.emit({
      childEvent: event, 
      parentColor: this.childColorValue
    });

  }


}
