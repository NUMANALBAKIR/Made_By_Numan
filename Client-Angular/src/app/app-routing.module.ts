import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { StudentsCRUDComponent } from './students-crud/students-crud.component';

const routes: Routes = [
  { path: 'about', component: AboutComponent },
  { path: 'studentscrud', component: StudentsCRUDComponent },
  { path: '', redirectTo: 'about', pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }