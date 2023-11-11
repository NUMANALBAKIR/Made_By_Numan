import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  {

  user: User;
  getMessage: boolean;


  constructor(private loginService: LoginService, private router: Router) {
    this.user = new User();
    this.getMessage = false;
  }

  onLoginClick(event: any) {

    this.router.navigate(['/about']);

    // this.loginService.Login(this.user).subscribe(
    //   (response: User) => {
    //     this.user = response;
    //     this.router.navigateByUrl('/about');   // alternative
    //   },
    //   (error) => {
    //     console.log(error);
    //   }
    // );
  }


  // get message from directive
  onMessageClick(event: any) {
    this.getMessage = !this.getMessage;
  }

}
