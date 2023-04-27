import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/login/login.component';
import { BadgesComponent } from './admin/components/badges/badges.component';
import { ParentComponent } from './components/parent/parent.component';
import { ChildComponent } from './components/child/child.component';
import { GrandChildComponent } from './components/grand-child/grand-child.component';
import { GrandChild2Component } from './components/grand-child2/grand-child2.component';
import { GrandChild3Component } from './components/grand-child3/grand-child3.component';
import { LoginAlertsDirective } from './directives/login-alerts.directive';
import { AboutComponent } from './components/about/about.component';
import { SharedModule } from './shared/shared.module';
import { AdminModule } from './admin/admin.module';

@NgModule({
  declarations: [
    AppComponent,
    BadgesComponent,
    ParentComponent,
    ChildComponent,
    GrandChildComponent,
    GrandChild2Component,
    GrandChild3Component,
    LoginComponent,
    LoginAlertsDirective,
    AboutComponent

  ],
  imports: [
    SharedModule,
    AppRoutingModule,
    AdminModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
