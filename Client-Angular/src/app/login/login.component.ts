import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';
import { User } from '../user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  
  user: User;
  loginError: boolean;

  constructor(private loginService: LoginService, private router: Router) {
    this.user = new User();
    this.loginError = false;
  }

  ngOnInit(): void {
  }

  // onLoginClick(event: any) {
  //   this.loginService.Login(this.user).subscribe(
  //     (response: User) => {
  //       this.user = response;
  //       this.router.navigateByUrl('/about');
  //     },
  //     (error) => {
  //       console.log(error);
  //     }
  //   );
  // }

  onSubmitClick(event: any) {

    if (this.user.username == '' || this.user.password == '') {
      this.loginError = true;
    }
    else  {
      this.loginError = false;
    }

  }

}
