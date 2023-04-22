import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AdminModule } from './admin/admin.module';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BadgesComponent } from './badges/badges.component';
import { ParentComponent } from './parent/parent.component';
import { ChildComponent } from './child/child.component';
import { GrandChildComponent } from './grand-child/grand-child.component';
import { GrandChild2Component } from './grand-child2/grand-child2.component';
import { GrandChild3Component } from './grand-child3/grand-child3.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    BadgesComponent,
    ParentComponent,
    ChildComponent,
    GrandChildComponent,
    GrandChild2Component,
    GrandChild3Component
  ],
  imports: [
    BrowserModule,
    AdminModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
