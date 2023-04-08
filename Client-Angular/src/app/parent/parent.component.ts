import { Component, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from '../component-communications.service';
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

  ngOnInit(): void {
  }

  // receive emitter upon child button click 
  parentsMethod(event: any) {
    this.parentColorValue = event.parentColor;
  }


  // parent => comp.Commu service=> both grandChilds
  TurnBothRed(){
    this._compCommuService.turnRed();
  }


}
