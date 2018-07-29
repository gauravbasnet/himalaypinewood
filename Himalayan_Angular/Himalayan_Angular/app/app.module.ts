import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Route, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PublicComponent } from './component/public/public'

//ok
const routePath: Route[] = [

    {
        path: '', component: PublicComponent
    }
]

@NgModule({
    imports: [BrowserModule,
        RouterModule.forRoot(routePath, { useHash: true })
    ],
  declarations: [
      AppComponent,
      PublicComponent
  ],



  bootstrap:    [ AppComponent ]
})
export class AppModule { }
