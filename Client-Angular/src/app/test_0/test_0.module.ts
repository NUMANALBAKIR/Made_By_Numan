import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Test_0Component } from './test_0.component';
import { TestComponent } from './test/test.component';
import { TestChildComponent } from './test-child/test-child.component';
import { Test_0RoutingModule } from './test_0.routing';
import { SharedModule } from '../shared/shared.module';
import { ExceptionsComponent } from './exceptions/exceptions.component';
import { ExceptionsInterceptor } from '../exceptions.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
  declarations: [
    Test_0Component,
    TestComponent,
    TestChildComponent,
    ExceptionsComponent

  ],
  imports: [
    SharedModule,
    Test_0RoutingModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ExceptionsInterceptor, multi: true}
  ]
})
export class Test_0Module { }
