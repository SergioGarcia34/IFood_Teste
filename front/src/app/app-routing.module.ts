import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { CadastroComponent } from './core/components/cadastro/cadastro.component'
import { ConsultaComponent } from './core/components/consulta/consulta.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ErrorAuthComponent } from './core/components/error-auth/error-auth.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'cadastro', component: CadastroComponent,  canActivate: [AuthGuard] },
  { path: 'produtos', component: ConsultaComponent,  canActivate: [AuthGuard] },
  { path: 'error', component: ErrorAuthComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'login'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
