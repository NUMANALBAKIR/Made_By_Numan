import { Component, OnDestroy, OnInit } from '@angular/core';
import { ComponentCommunicationsService } from '../../services/component-communications.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-grand-child2',
  templateUrl: './grand-child2.component.html',
  styleUrls: ['./grand-child2.component.css']
})
export class GrandChild2Component implements OnInit, OnDestroy {

  constructor(private _compCommuService: ComponentCommunicationsService) { }


  grandChild2Color = 'white';
  white = true;
  subscription = new Subscription();


  ngOnInit(): void {

    // child => service => this grandchild2
    this._compCommuService.observableChild.subscribe(
      (color: string) => {
        this.grandChild2Color = color;
      },
      (e) => {
        console.log(e);
      }
    );

    this.subscription =
      this._compCommuService.subjectParent.subscribe(
        (color) => {
          this.grandChild2Color = color;
        }
      );

  }


  toggleBlackWhite() {
    if (this.white) {
      this.grandChild2Color = 'pink';
      this.white = !this.white;
    } else {
      this.grandChild2Color = 'white';
      this.white = !this.white;
    }
  }


  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
