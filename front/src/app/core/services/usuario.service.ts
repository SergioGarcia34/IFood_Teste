import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {


  private url = environment.API;
  private httpOptions;
 

  constructor(private http: HttpClient) { 

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


  validarToken(token){ //colocar nos guards!!!!!!
    
    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + token });
    
    return this.http.get(`${this.url}/api/token`, { headers: this.httpOptions, 'observe': 'response' })
      .pipe(
        map((data : any) => {
          return data;
        }),
        catchError(error => {
          return throwError(error)
        })
      )
  }
}