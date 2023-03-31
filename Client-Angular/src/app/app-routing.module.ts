import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './admin/about/about.component';
import { AddStudentComponent } from './admin/add-student/add-student.component';
import { DeleteStudentComponent } from './admin/delete-student/delete-student.component';
import { EditStudentComponent } from './admin/edit-student/edit-student.component';
import { StudentsCRUDComponent } from './admin/students-crud/students-crud.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: 'studentscrud', pathMatch: 'full' },
  { path: 'about', component: AboutComponent },
  { path: 'studentscrud', component: StudentsCRUDComponent },
  { path: 'addstudent', component: AddStudentComponent },
  { path: 'editstudent', component: EditStudentComponent },
  { path: 'deletestudent', component: DeleteStudentComponent },
  { path: 'login', component: LoginComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }