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

    // parent => service subject => this grandchild2
    this.subscription =
      this._compCommuService.subject.subscribe(
        (color) => {
          this.grandChild2Color = color;
        }
      );

  }


  togglePinkWhite() {
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
