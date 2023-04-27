import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User;
  getMessage: boolean;


  constructor(private loginService: LoginService, private router: Router) {
    this.user = new User();
    this.getMessage = false;
  }


  ngOnInit(): void {
  }


  onLoginClick(event: any) {
    this.loginService.Login(this.user).subscribe(
      (response: User) => {
        this.user = response;
        this.router.navigateByUrl('/about');
      },
      (error) => {
        console.log(error);
      }
    );
  }


  // get message from directive
  onMessageClick(event: any) {
    this.getMessage = !this.getMessage;
  }

}
