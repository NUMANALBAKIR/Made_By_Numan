import { Directive, DoCheck, ElementRef, Input, OnInit } from '@angular/core';

/*
  Custom Directive 
  used in login page
*/

@Directive({
  selector: '[appLoginAlerts]'
})
export class LoginAlertsDirective implements DoCheck {

  @Input('errorInLogin') errorInLogin: boolean;
  errorMessage: string;

  constructor(private elementRef: ElementRef) {
    this.errorInLogin = false;
    this.errorMessage = 'Custom Directive says: Both email and password must be filled.';
  }

  ngDoCheck(): void {

    if (this.errorInLogin) {
      this.elementRef.nativeElement.innerHTML = `             
         <h4 style="color: red;">
            ${this.errorMessage}
         </h4>     
      `;
    } else {
      this.elementRef.nativeElement.innerHTML = '';
    }

  }


}
