import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgeValidatorDirective } from '../directives/age-validator.directive';
import { GenderCountryValidatorDirective } from '../directives/gender-country-validator.directive';
import { StudentIdUniqueValidatorDirective } from '../directives/student-id-unique-validator.directive';
import { FilterPipe } from '../pipes/filter.pipe';
import { ToUpperPipe } from '../pipes/to-upper.pipe';


@NgModule({
  declarations: [
    FilterPipe,
    ToUpperPipe,
    AgeValidatorDirective,
    GenderCountryValidatorDirective,
    StudentIdUniqueValidatorDirective

  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule

  ],
  exports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    ToUpperPipe,
    FilterPipe,
    GenderCountryValidatorDirective,
    AgeValidatorDirective,
    StudentIdUniqueValidatorDirective
  ]
})
export class SharedModule { }
