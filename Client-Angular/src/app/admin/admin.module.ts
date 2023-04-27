import { NgModule } from '@angular/core';
import { StudentsCRUDComponent } from './components/students-crud/students-crud.component';
import { AddStudentComponent } from './components/add-student/add-student.component';
import { EditStudentComponent } from './components/edit-student/edit-student.component';
import { ReadStudentComponent } from './components/read-student/read-student.component';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    StudentsCRUDComponent,
    AddStudentComponent,
    EditStudentComponent,
    ReadStudentComponent
  ],
  imports: [
    SharedModule,
    // RouterModule, // no need because using AdminRoutingModule below
    AdminRoutingModule
  ],
  exports: [
  ],
  providers: []
})
export class AdminModule { }
