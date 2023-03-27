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

  constructor(private loginService: LoginService, private router: Router) { }

  user: User = new User();

  ngOnInit(): void {
  }

  onLoginClick(event: any) {
    this.loginService.Login(this.user).subscribe(
      (r: User) => {
        this.user = r;
        this.router.navigateByUrl('/about');
      },
      (e) => {
        console.log(e);
      }
    );
  }

}
