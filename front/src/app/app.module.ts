import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule} from 'ngx-toastr';
import { MatIconModule } from '@angular/material/icon';
import { NgxCurrencyModule } from 'ngx-currency';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from "angular-datatables";
import { MatDialogModule } from '@angular/material/dialog';

import { ToastrService } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { LoginComponent } from './core/components/login/login.component';
import { CadastroComponent } from './core/components/cadastro/cadastro.component'
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { ConsultaComponent } from './core/components/consulta/consulta.component';
import { ModalComponent } from './core/components/modal/modal.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CadastroComponent,
    NavbarComponent,
    ConsultaComponent,
    ModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule, 
    BrowserAnimationsModule,
    FormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      progressBar: true,
      closeButton: true,
      timeOut: 8000 
    }),
    HttpClientModule,
    MatIconModule,
    NgxCurrencyModule,
    DataTablesModule,
    MatDialogModule
  ],
  providers: [ToastrService],
  bootstrap: [AppComponent]
})
export class AppModule { }
