import { Component, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from 'src/app/services/component-communications.service';

@Component({
  selector: 'app-grand-child3',
  templateUrl: './grand-child3.component.html',
  styleUrls: ['./grand-child3.component.css']
})
export class GrandChild3Component implements OnInit {

  constructor(private compCommuService:ComponentCommunicationsService) { }


  grandChild3Color = 'white';


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

}
