import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, Component, ContentChild, ContentChildren, DoCheck, ElementRef, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, QueryList, SimpleChanges, ViewChild, ViewChildren } from '@angular/core';
import { GrandChildComponent } from '../grand-child/grand-child.component';
import { GrandChild2Component } from '../grand-child2/grand-child2.component';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { GrandChild3Component } from '../grand-child3/grand-child3.component';
import { GrandChild4Component } from '../grand-child4/grand-child4.component';


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
  // @ViewChild('gc2') gc2: GrandChild2Component = {} as GrandChild2Component; //unused
  @ViewChildren('gc12') gc12 = {} as QueryList<GrandChildComponent>;

  @ContentChild('gc3FromParent') gc3FromParent = {} as GrandChild3Component;
  // @ContentChild('gc3FromParent') gc4FromParent = {} as GrandChild4Component; //unused
  @ContentChildren('gc34FromParent', { descendants: true }) gc34FromParent = {} as QueryList<GrandChild3Component>;


  constructor(private _compCommuService: ComponentCommunicationsService) {
  }


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
    this.gc3FromParent.grandChild3Color = 'green';
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


  // viewChildren
  toggle12PinkWhite() {
    const gc12Arr = this.gc12.toArray();
    gc12Arr.forEach(element => {
      element.togglePinkWhite();
    });
  }


  // using contentChild
  getGC3Color() {
    this.childColor = this.gc3FromParent.grandChild3Color;
  }


  // using contentChildren
  getToggleGC34Color() {
    const gc34Arr = this.gc34FromParent.toArray();

    if (this.childColor != gc34Arr[0].itsColor) {
      this.childColor = gc34Arr[0].itsColor;
    }
    else {
      this.childColor = gc34Arr[1].itsColor;
    }
  }


}
