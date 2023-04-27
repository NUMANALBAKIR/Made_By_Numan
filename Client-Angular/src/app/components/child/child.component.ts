import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, Component, ContentChild, DoCheck, ElementRef, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { GrandChildComponent } from '../grand-child/grand-child.component';
import { GrandChild2Component } from '../grand-child2/grand-child2.component';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { GrandChild3Component } from '../grand-child3/grand-child3.component';


@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements
  OnChanges,
  OnInit,
  DoCheck,
  AfterContentInit,
  AfterContentChecked,
  AfterViewInit,
  AfterViewChecked,
  OnDestroy {

  @Input('childColor') childColor: string = '';
  colorForGrandChild: string = '';
  @Output('childsClickEmitter') childsClickEmitter = new EventEmitter<any>();

  @ViewChild('gc1') gc1: GrandChildComponent = {} as GrandChildComponent;
  @ViewChild('gc2') gc2: GrandChild2Component = {} as GrandChild2Component;
  @ViewChildren('gcs') gcs = {} as QueryList<GrandChildComponent>;

  @ContentChild('grandChild3FromParent') grandChild3FromParent = {} as GrandChild3Component;


  constructor(private _compCommuService: ComponentCommunicationsService) {
  }


  // Life-cycle hooks
  ngOnChanges(changes: SimpleChanges): void {
    console.log('=> ngOnChanges() called.');
    // for (let propName in changes) { // notice 'in', instead of 'of' in template
    //   let change = changes[propName];
    //   let previous = change.previousValue;
    //   let current = JSON.stringify(change.currentValue);
    //   console.log('current: ' + current + 
    //   '. previous: ' + previous );
    // }

    // // modifying @input
    // if (changes['childColor']) {
    //   this.childColor = '#ffc0cb';
    //   console.log(
    //     'modified: ' + this.childColor
    //     );
    // }
  }
  ngOnInit(): void {
    this.grandChild3FromParent.grandChild3Color = 'green';
    console.log('=> ngOnInit() called.');
  }
  ngDoCheck(): void {
    console.log('=> ngDoCheck() called.');
  }
  ngAfterContentInit(): void {
    console.log('=> ngAfterContentInit() called.');
  }
  ngAfterContentChecked(): void {
    console.log('=> ngAfterContentChecked() called.');
  }
  ngAfterViewInit(): void {
    console.log('=> ngAfterViewInit() called.');
  }
  ngAfterViewChecked(): void {
    console.log('=> ngAfterViewChecked() called.');
  }
  ngOnDestroy(): void {
    console.log('=> ngOnDestroy() called.');
  }
  // Life-cycle hooks end.


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


  getGC3Color() {
    this.childColor = this.grandChild3FromParent.grandChild3Color;
  }




}
