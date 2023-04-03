import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AdminModule } from './admin/admin.module';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgeValidatorDirective } from './age-validator.directive';
import { GenderCountryValidatorDirective } from './gender-country-validator.directive';
import { StudentIdUniqueValidatorDirective } from './student-id-unique-validator.directive';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AgeValidatorDirective,
    GenderCountryValidatorDirective,
    StudentIdUniqueValidatorDirective
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
