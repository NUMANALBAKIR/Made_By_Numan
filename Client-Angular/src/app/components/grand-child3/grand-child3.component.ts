import { AfterContentChecked, Component, DoCheck, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from 'src/app/services/component-communications.service';

@Component({
  selector: 'app-grand-child3',
  templateUrl: './grand-child3.component.html',
  styleUrls: ['./grand-child3.component.css']
})
export class GrandChild3Component implements OnInit, DoCheck {

  constructor(private compCommuService:ComponentCommunicationsService) {
    this.grandChild3Color = 'white';
    this.itsColor = 'white';
   }


  grandChild3Color: string;
  itsColor: string;


  ngOnInit(): void {
    // parent => service => this grandchild3
    this.compCommuService.observableChild.subscribe(
      (color: string) => {
        this.grandChild3Color = color;
      },
      (e) => {
        console.log(e);
      }
    );
  }


  ngDoCheck(): void {
    this.itsColor = this.grandChild3Color;
  }


}
