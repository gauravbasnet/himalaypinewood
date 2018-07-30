import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Route, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PublicComponent } from './component/public/public';
import { AdminComponent } from './component/admin/admin.component';
import { DashboardComponent } from './component/admin/dashboard/dashboard.component'


//ok
const routePath: Route[] = [

    {
        path: '', component: PublicComponent
    },
    {
        path: 'admin', component: AdminComponent,
        children: [
            { path: 'dashboard', component: DashboardComponent }
        ]
    }
   
]

@NgModule({
    imports: [BrowserModule,
        RouterModule.forRoot(routePath, { useHash: true })
    ],
  declarations: [
      AppComponent,
      PublicComponent,
      AdminComponent,
      DashboardComponent
  ],



  bootstrap:    [ AppComponent ]
})
export class AppModule { }
