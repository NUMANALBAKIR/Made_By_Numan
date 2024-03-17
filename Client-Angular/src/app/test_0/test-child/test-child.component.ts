import { Component, OnDestroy, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-test-child',
  templateUrl: './test-child.component.html',
  styleUrls: ['./test-child.component.css']
})
export class TestChildComponent implements OnInit, OnDestroy {

  months: any[] = [];
  name: string;
  age: number;
  sub1: Subscription | any;


  constructor(private sharedService: SharedService, public activatedRoute: ActivatedRoute) {

    this.name = '';
    this.age = 0;


    this.sub1 = this.sharedService.getMonthsEmitter().subscribe(
      (r) => {
        this.months = r;
      },
      e => {
        console.log(e);
      },
      () => {
        console.log("test child complpete.");
      }
    );

    const sub2 = this.activatedRoute.queryParams.subscribe(
      r => {
        this.name = r.name || 'defaultString';
        this.age = r.age;
      }
    );
    this.sub1.add(sub2);
  }


  ngOnInit() {
  }


  ngOnDestroy() {
    this.sub1.unsubscribe();
  }
}
