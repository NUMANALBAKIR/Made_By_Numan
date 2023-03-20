import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';
import { StudentsService } from '../students.service';

@NgModule({
  declarations: [
    AboutComponent,
    StudentsCRUDComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    AboutComponent,
    StudentsCRUDComponent
  ],
  providers: [StudentsService]
})
export class AdminModule { }
