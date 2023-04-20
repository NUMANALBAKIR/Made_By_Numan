import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './admin/about/about.component';
import { AddStudentComponent } from './admin/add-student/add-student.component';
import { EditStudentComponent } from './admin/edit-student/edit-student.component';
import { StudentsCRUDComponent } from './admin/students-crud/students-crud.component';
import { LoginComponent } from './login/login.component';
import { BadgesComponent } from './badges/badges.component';
import { ParentComponent } from './parent/parent.component';

const routes: Routes = [
  { path: '', redirectTo: 'studentscrud', pathMatch: 'full' },
  { path: 'about', component: AboutComponent },
  { path: 'studentscrud', component: StudentsCRUDComponent },
  { path: 'addstudent', component: AddStudentComponent },
  { path: 'editstudent', component: EditStudentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'badges', component: BadgesComponent },
  { path: 'componentcommunications', component: ParentComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }