import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = '';
  today: Date = new Date();

  constructor(public loginService: LoginService) { }

  ngOnInit(): void {
    this.title = 'Client-Angular';
  }

  onLogoutClick(event: any) {
    this.loginService.Logout();
  }

}
