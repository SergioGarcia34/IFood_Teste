import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProdutoService } from '../../services/produto.service'
import { UtilsService } from '../../services/utils.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})

export class CadastroComponent implements OnInit {

 
  public cadastroForm : FormGroup;
  public isSubmitted;
  public url = false;
  public imgSize = false;
  
 
  constructor(private formBuilder: FormBuilder, public utils: UtilsService, private produto : ProdutoService) { }

  createForm(){

    this.url = null;
    this.cadastroForm  =  this.formBuilder.group({
      NomProd: [null, Validators.required],
      VlVend: [null, Validators.required],
      StrImag: ['', Validators.required],
      File: [null],
      DescProd: [''],
    });

  }

  ngOnInit(): void {
    this.isSubmitted = false;
    this.createForm();
  }

  preview(event) {
      
    this.url = null;
    this.cadastroForm.get('StrImag').setValue(null);
    let size = event.target.files[0].size;

    if(size <= 5240000 ) {//5mb 

      if (event.target.files && event.target.files[0]) {
        var reader = new FileReader();
        reader.onload = (event: any) => {
            this.url = event.target.result;
            this.cadastroForm.get('StrImag').setValue(btoa(reader.result.toString()))
        }
        this.imgSize = false;
        reader.readAsDataURL(event.target.files[0]);
      }
    }
    else {
      this.imgSize = true;
      this.cadastroForm.get('File').setValue(null);
    }
  }
  




  onSubmit() {
    
    this.isSubmitted = true;

    if(!this.cadastroForm.invalid){
           
   
       this.produto.postProduto(this.cadastroForm.value).subscribe(async (resp: Response) => {
       
       this.utils.showSuccess("Cadastro efetuado com sucesso", null);
       this.isSubmitted = false;
       this.createForm();

     }, async (err) => { 
       this.utils.showError("Não foi possível realizar o cadastro do produto", err.message);
     } );
    } 

    
     return;
 }
}
