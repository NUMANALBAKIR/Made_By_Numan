import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client-Angular';
  thisYear: number = new Date().getFullYear();

  // let student = {
  //   id: 1,
  //   name: "Numan",
  //   dob: "1-1-2000",
  //   age: 23,
  //   gender: "male",
  //   class: 5,
  // }
  
}
