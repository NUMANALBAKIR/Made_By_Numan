import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DeleteStudentComponent } from './delete-student/delete-student.component';
import { StudentComponent } from './student/student.component';
import { ToUpperPipe } from './to-upper.pipe';


@NgModule({
  declarations: [
    AboutComponent,
    StudentsCRUDComponent,
    AddStudentComponent,
    EditStudentComponent,
    DeleteStudentComponent,
    StudentComponent,
    ToUpperPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    AboutComponent,
    StudentsCRUDComponent
  ],
  providers: []
})
export class AdminModule { }
