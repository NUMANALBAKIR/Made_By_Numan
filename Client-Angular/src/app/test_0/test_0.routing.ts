import { Routes, RouterModule } from '@angular/router';
import { TestComponent } from './test/test.component';
import { TestChildComponent } from './test-child/test-child.component';
import { NgModule } from '@angular/core';
import { Test_0Component } from './test_0.component';
import { ExceptionsComponent } from './exceptions/exceptions.component';
import { ExceptionsInterceptor } from '../exceptions.interceptor';

const routes: Routes = [
  { path: '', pathMatch: 'full' },
  // { path: 'test_0', component: Test_0Component },
  { path: 'test', component: TestComponent },
  { path: 'testChild', component: TestChildComponent },
  { path: 'exceptions', component: ExceptionsComponent },

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class Test_0RoutingModule { }
