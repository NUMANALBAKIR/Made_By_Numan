import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToUpperPipe } from './to-upper.pipe';
import { FilterPipe } from './filter.pipe';
import { GenderCountryValidatorDirective } from './gender-country-validator.directive';
import { AgeValidatorDirective } from './age-validator.directive';
import { StudentIdUniqueValidatorDirective } from './student-id-unique-validator.directive';
import { ReadStudentComponent } from './read-student/read-student.component';
import { HighchartsChartModule } from 'highcharts-angular';

@NgModule({
  declarations: [
    StudentsCRUDComponent,
    AddStudentComponent,
    EditStudentComponent,
    ToUpperPipe,
    FilterPipe,
    GenderCountryValidatorDirective,
    AgeValidatorDirective,
    StudentIdUniqueValidatorDirective,
    ReadStudentComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    HighchartsChartModule
  ],
  exports: [
    StudentsCRUDComponent
  ],
  providers: []
})
export class AdminModule { }
