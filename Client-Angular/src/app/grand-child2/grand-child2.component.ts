import { Component, OnInit } from '@angular/core';
import { StudentsService } from '../students.service';
import { ComponentCommunicationsService } from '../component-communications.service';

@Component({
  selector: 'app-grand-child2',
  templateUrl: './grand-child2.component.html',
  styleUrls: ['./grand-child2.component.css']
})
export class GrandChild2Component implements OnInit {

  constructor(private _compCommuService: ComponentCommunicationsService) { }

  grandChild2Color: string = 'white';
  white = true;


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
  }


  toggleBlackWhite() {
    if (this.white) {
      this.grandChild2Color = 'black';
      this.white = !this.white;
    } else {
      this.grandChild2Color = 'white';
      this.white = !this.white;
    }
  }



}
