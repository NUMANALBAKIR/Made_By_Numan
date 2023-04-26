import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AddStudentComponent } from './admin/add-student/add-student.component';
import { EditStudentComponent } from './admin/edit-student/edit-student.component';
import { StudentsCRUDComponent } from './admin/students-crud/students-crud.component';
import { LoginComponent } from './login/login.component';
import { BadgesComponent } from './badges/badges.component';
import { ParentComponent } from './parent/parent.component';
import { ReadStudentComponent } from './admin/read-student/read-student.component';
import { CanActivateGuardService } from './admin/can-activate-guard.service';

const routes: Routes = [
  { path: '', redirectTo: 'admin/studentscrud', pathMatch: 'full' },
  { path: 'about', component: AboutComponent },
  { path: 'componentcommunications', component: ParentComponent },
  { path: 'login', component: LoginComponent },

  {
    path: 'admin',
    // canActivate: [CanActivateGuardService], data: { expectedRole: 'Admin' },
    children:
      [
        { path: 'studentscrud', component: StudentsCRUDComponent },
        { path: 'addstudent', component: AddStudentComponent },
        { path: 'read/student/:studentId/:studentName', component: ReadStudentComponent },
        { path: 'editstudent', component: EditStudentComponent },
        { path: 'badges', component: BadgesComponent }
      ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }