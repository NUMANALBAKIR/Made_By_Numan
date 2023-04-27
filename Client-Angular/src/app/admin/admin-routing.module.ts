import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddStudentComponent } from './components/add-student/add-student.component';
import { BadgesComponent } from './components/badges/badges.component';
import { EditStudentComponent } from './components/edit-student/edit-student.component';
import { ReadStudentComponent } from './components/read-student/read-student.component';
import { StudentsCRUDComponent } from './components/students-crud/students-crud.component';
import { CanDeactivateGuardService } from './guards/can-deactivate-guard.service';


const routes: Routes = [
  { 
    // data: { expectedRole: 'Admin' },
    // canActivate: [CanActivateGuardService],
    path: '', children:
      [
        { path: 'studentscrud', component: StudentsCRUDComponent },
        { path: 'addstudent', component: AddStudentComponent, canDeactivate: [CanDeactivateGuardService] },
        { path: 'readstudent/:studentId/:studentName', component: ReadStudentComponent },
        { path: 'editstudent', component: EditStudentComponent, canDeactivate: [CanDeactivateGuardService] },
        { path: 'badges', component: BadgesComponent }
      ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes) // combining 'routes' with the imported
  ],
  exports: [RouterModule] // made available to AdminModule
})
export class AdminRoutingModule { }
