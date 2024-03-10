// import { DashboardService } from '../../../services/dashboard.service';
import { AfterViewChecked, AfterViewInit, Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { SharedService } from '../services/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit, AfterViewInit, AfterViewChecked {






  toTestChild(): void{
    this.router.navigate(['/testChild'], {queryParams: {name: 'numan', age: 32}})
  }

  // private sub: Subscription;

  //---

  public MONTHS$ = this.sharedService.MONTHS_BS.asObservable();

  trackByFn(i: number, item: any) {
    return item.Name;
  }

  //-----

  @ViewChild('test') test: ElementRef | any;

  constructor(
    private renderer: Renderer2,
    private sharedService: SharedService,
    private router: Router
  ) {
  }


  ngOnInit(): void {
 this.sharedService.getMonths().subscribe(
      (r) => {
        this.sharedService.dataStore.data = r;

        this.sharedService.MONTHS_BS.next(
          Object.assign({}, this.sharedService.dataStore).data
          // r // this is like: .next(r)
        );


        this.sharedService.emitMonths(r);
      }
    );

  }


  ngAfterViewInit(): void {
    let el = this.test.nativeElement;

    // this.renderer.setStyle(el, 'backgroundColor', 'red');
    this.renderer.listen(el, 'click', () => { alert('clicked'); })
    this.renderer.addClass(el, 'mb-5');
    el.innerText = 'sss';

    const text = this.renderer.createText('Click here to add ');
    this.renderer.appendChild(el, text);
    this.renderer.setAttribute(el, 'class', 'text-warning');

  }

  ngAfterViewChecked(): void {
    // alert('ngAfterViewChecked');
  }


  ngAftrVw() {
    let el = this.test.nativeElement;
    this.renderer.setStyle(el, 'backgroundColor', 'blue');
  }

  justEvent(): void {
    alert('justEvent');
  }



}
