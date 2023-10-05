import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, Component, ContentChild, ContentChildren, DoCheck, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { GrandChildComponent } from '../grand-child/grand-child.component';
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

  colorForGrandChild: string = '';
  @Input('childColor') childColor: string = '';
  @Output('childsClickEmitter') childsClickEmitter = new EventEmitter<any>();

  @ViewChild('gc1') gc1: GrandChildComponent = {} as GrandChildComponent;
  @ViewChildren('gc12') gc12 = {} as QueryList<GrandChildComponent>;

  @ContentChild('gc3FromParent') gc3FromParent = {} as GrandChild3Component;
  @ContentChildren('gc34FromParent', { descendants: true }) gc34FromParent = {} as QueryList<GrandChild3Component>;


  constructor() { }


  // Life-cycle hooks start
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


  // Emits Output to its parent
  onClickToParent(event: any) {
    this.childsClickEmitter.emit({
      childEvent: event,
      childsColor: this.childColor
    });
  }


  // viewChildren
  // similarly, you can access ViewChildrens' data
  toggle12PinkWhite() {
    const gc12Arr = this.gc12.toArray();
    gc12Arr.forEach(gc => {
      gc.togglePinkWhite();
    });
  }


  // using contentChild
  // similarly, you can toggle ContentChild's method
  getGC3Color() {
    this.childColor = this.gc3FromParent.grandChild3Color;
  }


  // using contentChildren
  // similarly, you can toggle ContentChildrens' methods
  getToggleGC34Color() {
    const gc34Arr = this.gc34FromParent.toArray();

    // just practise
    this.childColor =
      (this.childColor === gc34Arr[0].itsColor) ? gc34Arr[1].itsColor : gc34Arr[0].itsColor;

    // above is alternative
    // if (this.childColor != gc34Arr[0].itsColor) {
    //   this.childColor = gc34Arr[0].itsColor;
    // }
    // else {
    //   this.childColor = gc34Arr[1].itsColor;
    // }
  }


}
