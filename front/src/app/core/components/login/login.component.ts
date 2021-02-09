import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service'
import { UtilsService } from '../../services/utils.service'


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {


  public loginForm : FormGroup;
  public isSubmitted : boolean = false;


  constructor(private formBuilder: FormBuilder, private router:Router, private usuario : UsuarioService, private utils : UtilsService) { }

  ngOnInit(): void {

    this.isSubmitted = false;

    this.loginForm  =  this.formBuilder.group({
      usuario: [null, Validators.required],
      senha: [null, Validators.required]
    });

  }
  
  onSubmit() {
    
     this.isSubmitted = true;

     if(!this.loginForm.invalid){

      let strAuth = "Basic " + btoa(this.loginForm.value.usuario + ":" + this.loginForm.value.senha)
            
      this.usuario.postCredencial(strAuth).subscribe(async (resp: Response) => {
        
        localStorage.setItem("Token", resp.headers.get("Authorization"));
        this.router.navigate(['/cadastro']);

      }, async (err) => { 
        this.utils.showError("Não foi possível realizar a autenticação", err.message );
      });
     } 
     
      return;
  }
}