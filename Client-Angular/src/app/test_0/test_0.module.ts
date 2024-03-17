import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Test_0Component } from './test_0.component';
import { TestComponent } from './test/test.component';
import { TestChildComponent } from './test-child/test-child.component';
import { Test_0RoutingModule } from './test_0.routing';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    Test_0Component,
    TestComponent,
    TestChildComponent,

  ],
  imports: [
    SharedModule,
    Test_0RoutingModule,
  ]
})
export class Test_0Module { }
