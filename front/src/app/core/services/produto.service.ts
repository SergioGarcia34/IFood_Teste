import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UsuarioService } from './usuario.service';
import { UtilsService } from './utils.service';
import { Produto } from '../shared/models/Produto.models';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {


  private url = environment.API;
  private httpOptions;
 

  constructor(private http: HttpClient, private usuario: UsuarioService, private utils : UtilsService) {}

  
  postProduto(produto) {

    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + this.utils.getLocalStorage("Token") } );

    return this.http.post(`${this.url}/api/produtos`, JSON.stringify(produto),  { headers: this.httpOptions, 'observe': 'response' })
      .pipe(
        map((data : any) => {
          return data;
        }),
        catchError(error => {
          return throwError(error)
        })
      )
  }

  
  getProduto() {

    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + this.utils.getLocalStorage("Token")  });

    return this.http.get(`${this.url}/api/produtos`, { headers: this.httpOptions})
    .pipe(
      map((data : Produto) => {
        return data;
      }),
      catchError(error => {
        return throwError(error)
      })
    )
  }


  deleteProduto(id) {

    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + this.utils.getLocalStorage("Token")});

    return this.http.delete(`${this.url}/api/produtos/${id}`, { headers: this.httpOptions})
    .pipe(
      map((data : Produto) => {
        return data;
      }),
      catchError(error => {
        return throwError(error)
      })
    )
  }


  updateProduto(id, produto) {

    this.httpOptions = new HttpHeaders({ 'Content-Type': 'application/json', 'Authorization': "Bearer " + this.utils.getLocalStorage("Token")});

    return this.http.put(`${this.url}/api/produtos/${id}`, JSON.stringify(produto), { headers: this.httpOptions})
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


