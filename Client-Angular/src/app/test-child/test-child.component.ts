import { Component, OnInit } from '@angular/core';
import { SharedService } from '../services/shared.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-test-child',
  templateUrl: './test-child.component.html',
  styleUrls: ['./test-child.component.css']
})
export class TestChildComponent implements OnInit {

  months: any[] = [];
name: string;
age: number;


  constructor(private sharedService: SharedService, public activatedRoute: ActivatedRoute) {

    this.name='';
    this.age=0;


    const sub = this.sharedService.getMonthsEmitter().subscribe(
      (r) => {
        this.months = r;
      },
      e =>{
        console.log(e);
      },
      () => {
        console.log("test child complpete.");
      }
    );

    const sub2 = this.activatedRoute.queryParams.subscribe(
      r=>{
        this.name = r.name;
        this.age = r.age;

      }
    );



   }


  ngOnInit() {
  }

}
