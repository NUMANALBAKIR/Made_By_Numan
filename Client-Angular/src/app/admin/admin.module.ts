import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DeleteStudentComponent } from './delete-student/delete-student.component';


@NgModule({
  declarations: [
    AboutComponent,
    StudentsCRUDComponent,
    AddStudentComponent,
    EditStudentComponent,
    DeleteStudentComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ],
  exports: [
    AboutComponent,
    StudentsCRUDComponent
  ],
  providers: []
})
export class AdminModule { }
