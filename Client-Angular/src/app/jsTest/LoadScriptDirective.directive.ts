// src/app/directives/load-script.directive.ts
import { Directive, ElementRef, Input, OnInit, OnDestroy } from '@angular/core';

@Directive({
  selector: '[appLoadScript]'
})
export class LoadScriptDirective implements OnInit, OnDestroy {
  @Input() scriptUrl!: string;
  private scriptElement!: HTMLScriptElement;

  constructor(private el: ElementRef) {}

  ngOnInit(): void {
    this.loadScript();
  }

  ngOnDestroy(): void {
    this.unloadScript();
  }

  private loadScript(): void {
    this.scriptElement = document.createElement('script');
    this.scriptElement.src = this.scriptUrl;
    this.scriptElement.async = true;
    this.el.nativeElement.appendChild(this.scriptElement);
  }

  private unloadScript(): void {
    if (this.scriptElement) {
      this.el.nativeElement.removeChild(this.scriptElement);
    }
  }
}
