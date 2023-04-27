import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { ParentComponent } from './components/parent/parent.component';

const routes: Routes = [
  { path: '', redirectTo: 'admin/studentscrud', pathMatch: 'full' },

  { path: 'about', component: AboutComponent },
  { path: 'componentcommunications', component: ParentComponent },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  /* enableTracing: true, (this logs Router events)*/
  imports: [RouterModule.forRoot(routes, { useHash: true, onSameUrlNavigation: 'reload' })], // combining 'routes' with the imported
  exports: [RouterModule] // made available to AppModule
})
export class AppRoutingModule { }