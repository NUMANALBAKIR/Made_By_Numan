import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-grand-child',
  templateUrl: './grand-child.component.html',
  styleUrls: ['./grand-child.component.css']
})
export class GrandChildComponent implements OnInit, OnDestroy {

  constructor(private compCommuService: ComponentCommunicationsService) { }


  grandChildColor: string = 'white';
  white = true;
  subscriptions: Subscription[] = [];


  ngOnInit(): void {

    // parent => service observable => this grandchild2
    this.subscriptions.push(
      this.compCommuService.observableChild.subscribe(
        (color: string) => {
          this.grandChildColor = color;
        },
        (e) => {
          console.log(e);
        }
      )           // subsciptions are separated by comma
    );

    this.subscriptions.push(
      this.compCommuService.subjectParent.subscribe(
        (color) => {
          this.grandChildColor = color;
        }
      )
    );

  }


  togglePinkWhite() {
    if (this.white) {
      this.grandChildColor = 'pink';
      this.white = !this.white;
    } else {
      this.grandChildColor = 'white';
      this.white = !this.white;
    }
  }


  ngOnDestroy(): void {
    this.subscriptions.forEach(element => {
      element.unsubscribe();
    });
  }

}
