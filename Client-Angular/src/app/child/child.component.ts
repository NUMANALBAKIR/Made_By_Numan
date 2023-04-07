import { Component, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { GrandChildComponent } from '../grand-child/grand-child.component';
import { GrandChild2Component } from '../grand-child2/grand-child2.component';
import { StudentsService } from '../students.service';
import { ComponentCommunicationsService } from '../component-communications.service';


@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnInit {

  constructor(private _compCommuService: ComponentCommunicationsService) { }

  @Input('childColor') childColor: string = '';
  colorForGrandChild: string = '';
  @Output('childsClickEmitter') childsClickEmitter = new EventEmitter<any>();

  @ViewChild('gc1') gc1: GrandChildComponent = {} as GrandChildComponent;
  @ViewChild('gc2') gc2: GrandChild2Component = {} as GrandChild2Component;
  @ViewChildren('gcs') gcs = {} as QueryList<GrandChildComponent>;


  ngOnInit(): void {
  }


  // Emit Output to its parent
  onClickToParent(event: any) {
    this.childsClickEmitter.emit({
      childEvent: event,
      parentColor: this.childColor
    });
  }

  
  // viewchildren
  toggleBothBlackWhite() {
    let gcsArr = this.gcs.toArray();
    gcsArr.forEach(element => {
      element.toggleBlackWhite();
    });
  }


  // this child => service => grandchild2
  TurnBlueGrandChild2() {
    this._compCommuService.turnBlue();
  }



}
