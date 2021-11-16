import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

//Para hacer las peticiones AJAX
import { HttpClientModule } from '@angular/common/http';

//Para que funcione trabajar con <form>
import { FormsModule } from '@angular/forms';

import { routing, appRoutingProvider } from './app.routing';

import { AppComponent } from './app.component';
import { ParqueoComponent } from './components/parqueo/parqueo.component';
import { ErrorComponent } from './components/error/error.component';
import { HeaderComponent } from './components/header/header.component';
import { FiltrarPipe } from './pipes/filtrar.pipe';

import { NgbPaginationModule, NgbAlertModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    AppComponent,
    ParqueoComponent,
    ErrorComponent,
    HeaderComponent,

    FiltrarPipe,
  ],
  imports: [
    BrowserModule,

    routing,

    FormsModule,

    HttpClientModule,
    NgbModule,

    NgbPaginationModule, NgbAlertModule
  ],
  providers: [appRoutingProvider],
  bootstrap: [AppComponent]
})
export class AppModule { }
