import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { ParentComponent } from './components/parent/parent.component';
import { TestComponent } from './test/test.component';

const routes: Routes = [
  { path: '', redirectTo: 'about', pathMatch: 'full' },
  { path: 'about', component: AboutComponent },
  { path: 'componentcommunications', component: ParentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'test', component: TestComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) } // lazy loaded

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false /*, enableTracing:true (logs router events)*/, preloadingStrategy: PreloadAllModules })], // combining 'routes' with the imported
  exports: [RouterModule] // made available to AppModule
})
export class AppRoutingModule { }