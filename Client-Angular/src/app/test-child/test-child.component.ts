import { Component, OnInit } from '@angular/core';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-test-child',
  templateUrl: './test-child.component.html',
  styleUrls: ['./test-child.component.css']
})
export class TestChildComponent implements OnInit {

  constructor(private sharedService: SharedService) {
    const sub = this.sharedService.getMonthsEmitter().subscribe(
      (r) => {
        this.months = r;
      },
      e =>{
        console.log(e);
      },
      () => {
        console.log();
      }
    );

   }

  months: any[] = [];

  ngOnInit() {
  }

}
