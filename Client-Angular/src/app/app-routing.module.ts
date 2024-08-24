import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './components/login/login.component';
import { ParentComponent } from './components/parent/parent.component';
import { TestComponent } from './test_0/test/test.component';
import { TestChildComponent } from './test_0/test-child/test-child.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Test_0Component } from './test_0/test_0.component';
import { JsTestComponent } from './jsTest/jsTest.component';

const routes: Routes = [
  { path: '', redirectTo: 'about', pathMatch: 'full' },
  { path: 'about', component: AboutComponent },
  { path: 'componentcommunications', component: ParentComponent },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'jstest', component: JsTestComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) }, //lazy loaded //below is standard way
  { path: 'test_0', component: Test_0Component, loadChildren: async() => (await import('./test_0/test_0.module')).Test_0Module },
  { path: '**', redirectTo: 'about', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: false /*, enableTracing:true (logs router events)*/, preloadingStrategy: PreloadAllModules })], // combining 'routes' with the imported
  exports: [RouterModule] // made available to AppModule
})
export class AppRoutingModule { }