import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-grand-child',
  templateUrl: './grand-child.component.html',
  styleUrls: ['./grand-child.component.css']
})
export class GrandChildComponent implements OnInit, OnDestroy {

  constructor(private _compCommuService: ComponentCommunicationsService) { }


  grandChildColor: string = 'white';
  white = true;
  subscription = new Subscription();

  ngOnInit(): void {

    this.subscription =
      this._compCommuService.subjectParent.subscribe(
        (color) => {
          this.grandChildColor = color;
        }
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
    this.subscription.unsubscribe();
  }
}
