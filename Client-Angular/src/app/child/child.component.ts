import { AfterContentInit, Component, ContentChild, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { GrandChildComponent } from '../grand-child/grand-child.component';
import { GrandChild2Component } from '../grand-child2/grand-child2.component';
import { StudentsService } from '../students.service';
import { ComponentCommunicationsService } from '../component-communications.service';
import { GrandChild3Component } from '../grand-child3/grand-child3.component';


@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnInit,AfterContentInit {

  constructor(private _compCommuService: ComponentCommunicationsService) { }


  @Input('childColor') childColor: string = '';
  colorForGrandChild: string = '';
  @Output('childsClickEmitter') childsClickEmitter = new EventEmitter<any>();

  @ViewChild('gc1') gc1: GrandChildComponent = {} as GrandChildComponent;
  @ViewChild('gc2') gc2: GrandChild2Component = {} as GrandChild2Component;
  @ViewChildren('gcs') gcs = {} as QueryList<GrandChildComponent>;

  @ContentChild('grandChild3FromParent') grandChild3FromParent = {} as GrandChild3Component;


  ngOnInit(): void {
    this.grandChild3FromParent.grandChild3Color = 'green';

  }


  ngAfterContentInit(): void {
    
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


  getGC3Color(){
    this.childColor = this.grandChild3FromParent.grandChild3Color;
  }


}
