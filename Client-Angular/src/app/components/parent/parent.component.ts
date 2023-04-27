import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-parent',
  templateUrl: './parent.component.html',
  styleUrls: ['./parent.component.css']
})
export class ParentComponent implements OnInit {

  constructor(private _compCommuService: ComponentCommunicationsService) { }

  parentColorValue: string = '';
  toChildColorValue: string = '';


  @ViewChild('h1Ref') h1Ref = {} as ElementRef<HTMLInputElement>;


  ngOnInit(): void {
  }

  // receive emitter upon child button click 
  parentsMethod(event: any) {
    this.parentColorValue = event.parentColor;
  }


  // parent => comp.Commu service=> both grandChilds
  TurnBothRed() {
    this._compCommuService.turnRed();
  }

  // not working
  // setInput(){
  //   debugger;
  //   let thisElement = this.h1Ref.nativeElement;
  //   thisElement.innerText = 'This text was set using ElementRef.';
  // }

}
