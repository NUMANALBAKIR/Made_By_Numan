import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AdminModule } from './admin/admin.module';
import { HttpClientModule } from '@angular/common/http';
import { DeleteStudentComponent } from './admin/delete-student/delete-student.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgeValidatorDirective } from './admin/age-validator.directive';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AgeValidatorDirective
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
