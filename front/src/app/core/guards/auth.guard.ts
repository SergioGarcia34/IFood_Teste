import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { UsuarioService } from '../services/usuario.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private router: Router, private usuario : UsuarioService ){}

  private isAuth =  false;
  private isLoad = false;
 

 async canActivate(

      next: ActivatedRouteSnapshot,

      state: RouterStateSnapshot):  Promise< boolean | UrlTree >  {   

        let currentUrl = next.routeConfig.path;
        this.isAuth = await this.usuario.auth()

        if(this.isAuth){

          if(currentUrl.includes("error")){
            return this.router.parseUrl("/");

          } else {
            return true;

          }

        } else {

          if(this.isLoad){
              return true;

          }else{
              this.isLoad = true;
              return this.router.parseUrl("/error");
          }         
        }
    } 
}