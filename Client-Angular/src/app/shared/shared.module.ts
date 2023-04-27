import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgeValidatorDirective } from '../directives/age-validator.directive';
import { GenderCountryValidatorDirective } from '../directives/gender-country-validator.directive';
import { StudentIdUniqueValidatorDirective } from '../directives/student-id-unique-validator.directive';
import { FilterPipe } from '../pipes/filter.pipe';
import { ToUpperPipe } from '../pipes/to-upper.pipe';
import { HighchartsChartModule } from 'highcharts-angular';



@NgModule({
  declarations: [
    ToUpperPipe,
    FilterPipe,
    GenderCountryValidatorDirective,
    AgeValidatorDirective,
    StudentIdUniqueValidatorDirective

  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HighchartsChartModule

  ],
  exports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    HighchartsChartModule,

    ToUpperPipe,
    FilterPipe,
    GenderCountryValidatorDirective,
    AgeValidatorDirective,
    StudentIdUniqueValidatorDirective
  ]
})
export class SharedModule { }
