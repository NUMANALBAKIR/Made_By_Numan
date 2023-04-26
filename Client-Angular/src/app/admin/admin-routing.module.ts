import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';
import { AddStudentComponent } from './add-student/add-student.component';
import { BadgesComponent } from '../badges/badges.component';
import { EditStudentComponent } from './edit-student/edit-student.component';
import { ReadStudentComponent } from './read-student/read-student.component';
import { CanDeactivateGuardService } from '../can-deactivate-guard.service';

const routes: Routes = [
  {
    path: 'admin',
    // canActivate: [CanActivateGuardService], data: { expectedRole: 'Admin' },
    children:
      [
        { path: 'studentscrud', component: StudentsCRUDComponent },
        { path: 'addstudent', component: AddStudentComponent },
        { path: 'readstudent/:studentId/:studentName', component: ReadStudentComponent },
        { path: 'editstudent', component: EditStudentComponent, canDeactivate: [CanDeactivateGuardService] },
        { path: 'badges', component: BadgesComponent }
      ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes) // combining 'routes' with the imported
  ],
  exports: [RouterModule] // made available to AdminModule
})
export class AdminRoutingModule { }
