import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = '';
  year: number = 0;


  ngOnInit(): void {
    this.title = 'Client-Angular';
    this.year = new Date().getFullYear();

  }


}
