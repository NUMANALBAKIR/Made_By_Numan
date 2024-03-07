import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/login/login.component';
import { ParentComponent } from './components/parent/parent.component';
import { ChildComponent } from './components/child/child.component';
import { GrandChildComponent } from './components/grand-child/grand-child.component';
import { GrandChild2Component } from './components/grand-child2/grand-child2.component';
import { GrandChild3Component } from './components/grand-child3/grand-child3.component';
import { LoginAlertsDirective } from './directives/login-alerts.directive';
import { AboutComponent } from './components/about/about.component';
import { SharedModule } from './shared/shared.module';
import { BrowserModule } from '@angular/platform-browser';
import { GrandChild4Component } from './components/grand-child4/grand-child4.component';
import { TestComponent } from './test/test.component';
import { getBaseUrl } from 'src/main';
import { TestChildComponent } from './test-child/test-child.component';

@NgModule({
  declarations: [	
    AppComponent,
    AboutComponent,
    ParentComponent,
    ChildComponent,
    GrandChildComponent,
    GrandChild2Component,
    GrandChild3Component,
    GrandChild4Component,
    LoginComponent,
    LoginAlertsDirective,
    TestComponent,
      TestChildComponent
   ],
  imports: [
    BrowserModule,
    SharedModule,
    AppRoutingModule,
    // AdminModule //commented bcoz adding here eager loads. it's now lazy loaded in app.routing.module.
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
