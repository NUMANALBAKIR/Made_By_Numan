import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsCRUDComponent } from './components/students-crud/students-crud.component';
import { AddStudentComponent } from  './components/add-student/add-student.component';
import { EditStudentComponent } from './components/edit-student/edit-student.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToUpperPipe } from './pipes/to-upper.pipe';
import { FilterPipe } from './pipes/filter.pipe';
import { GenderCountryValidatorDirective } from './directives/gender-country-validator.directive';
import { AgeValidatorDirective } from './directives/age-validator.directive';
import { StudentIdUniqueValidatorDirective } from './directives/student-id-unique-validator.directive';
import { ReadStudentComponent } from './components/read-student/read-student.component';
import { HighchartsChartModule } from 'highcharts-angular';
import { AdminRoutingModule } from './admin-routing.module';

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
    // RouterModule, // no need because using AdminRoutingModule below
    AdminRoutingModule,
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
