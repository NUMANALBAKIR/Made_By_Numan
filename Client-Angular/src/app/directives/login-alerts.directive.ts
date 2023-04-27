import { Directive, DoCheck, ElementRef, HostBinding, HostListener, Input, OnInit, Renderer2 } from '@angular/core';

/*
  Custom Directive 
  used in login page
*/

@Directive({
  selector: '[appLoginAlerts]'
})
export class LoginAlertsDirective implements DoCheck {

  @Input('sendMessage') sendMessage: boolean;
  @HostBinding('title') title: string;
  message: string;
  h4Element: HTMLHeadingElement;
  spanElement: HTMLSpanElement;
  spanText: string;

  constructor(private elementRef: ElementRef, private renderer2: Renderer2) {
    this.sendMessage = false;
    this.message = 'Custom Directive says: Both email and password fields must be filled. On mouseenter, @HostListener adds black background.';
    this.title = 'This is title';

    this.h4Element =  this.renderer2.createElement('h4');
    this.spanElement = this.renderer2.createElement('span');
    this.spanText = this.renderer2.createText(this.message);    
  }


  ngDoCheck(): void {
    if (this.sendMessage == true) {
      this.renderer2.setStyle(this.h4Element, 'color', 'red');
      // this.renderer2.addClass(this.h4Element, 'myClass');// not working
      this.renderer2.setAttribute(this.h4Element, 'class', 'p-2');

      this.renderer2.appendChild(this.spanElement, this.spanText);
      this.renderer2.appendChild(this.h4Element, this.spanElement);
      this.renderer2.appendChild(this.elementRef.nativeElement, this.h4Element);

      // preferred way is above
      // this.elementRef.nativeElement.innerHTML = `             
      //    <h4 style="color:red;" class="p-2">
      //       <span>
      //         ${this.message}
      //       </span>
      //    </h4>     
      // `;
    }
    else {
      this.elementRef.nativeElement.innerHTML = '';
    }
  }


  @HostListener('mouseenter', ['$event'])
  onMouseEnter(event: any) {
    // this.elementRef.nativeElement.style.backgroundColor = 'black';// recommended below
    this.renderer2.setStyle(this.h4Element, 'background-color', 'black');
  }


  @HostListener('mouseleave', ['$event'])
  onMouseLeave(event: any) {
    // this.elementRef.nativeElement.style.backgroundColor = 'white';// recommended below
    this.renderer2.setStyle(this.h4Element, 'background-color', 'white');
  }



}
