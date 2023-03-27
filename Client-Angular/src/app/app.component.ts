import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from './student';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = '';
  today: Date =  new Date();


  ngOnInit(): void {
    this.title = 'Client-Angular';

  }

}
