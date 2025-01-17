import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UtilsService } from './utils.service'

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {


  private url = environment.API;
  private httpOptions;
 

  constructor(private http: HttpClient, private utils : UtilsService) { 

  }

  
  postCredencial(credencial) {

    this.httpOptions = new HttpHeaders({ 'Content-Type': 'text/json', 'Authorization': credencial } );
    
    return this.http.post(`${this.url}/api/login`, {}, {headers: this.httpOptions, 'observe': 'response' })
      .pipe(
        map((data : any) => {
          return data;
        }),
        catchError(error => {
          return throwError(error)
        })
      )
  }


  validarToken(token){ 
    
    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + token });
    
    return this.http.get(`${this.url}/api/login/token`, { headers: this.httpOptions, 'observe': 'response' })
      .pipe(
        map((data : any) => {
          return data;
        }),
        catchError(error => {
          return throwError(error)
        })
      )
  }


  auth(){
    
      let token = this.utils.getLocalStorage("Token");

      if (token){

          return this.validarToken(token).toPromise<boolean>(
              
          ).then(
            (res: any)=> {
                return true;          
          })
          .catch((err: any)=>{
                return false;
          })
      } else {
        return false;
      }
  }
}