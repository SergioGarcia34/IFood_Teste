import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'
import { ProdutoService } from '../../services/produto.service';
import { UtilsService } from '../../services/utils.service';
import { Produto } from '../../shared/models/Produto.models';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  public action : string;
  public local_data : Produto;
  public updateForm : FormGroup;
  public isSubmitted;
  public url : any;
  public imgSize;


  constructor(
    private formBuilder: FormBuilder, 
    public utils : UtilsService,
    private dialogRef: MatDialogRef<ModalComponent>,
    public produto : ProdutoService,
    @Optional()
    @Inject(MAT_DIALOG_DATA) public data : any)
    
    {this.action = data["action"],
      this.local_data = {
        IdProd: data["idProd"],
        NomProd: data["nomProd"],
        VlVend: data["vlVend"],
        DescProd: data["descProd"],
        StrImag: data["strImag"],
        DtInclusao: data["dtInclusao"]
      }
      this.url = atob(this.local_data.StrImag);

    }

  createForm(){

    this.updateForm  =  this.formBuilder.group({
      IdProd: [this.local_data.IdProd],
      NomProd: [this.local_data.NomProd, Validators.required],
      VlVend: [this.local_data.VlVend, Validators.required],
      File: [null],
      StrImag: [this.local_data.StrImag, Validators.required],
      DescProd: [this.local_data.DescProd],
      DtInclusao: [this.local_data.DtInclusao]
    });
  }
    
  excluir(){
    
    this.produto.deleteProduto(this.local_data.IdProd).subscribe(async (resp: any) => {

      this.utils.showSuccess("Produto deletado com sucesso", null);
      this.dialogRef.close({event: this.action, confirm: true});
     
    }, async (err) => { 

      this.utils.showError(err.message, "Não foi possível realizar a exclusão do produto");
      this.closeModal();
      return;
    });

    return;

  }  

  atualizar(){

    this.isSubmitted = true;

    if(!this.updateForm.invalid){
           
   
     this.produto.updateProduto(this.local_data.IdProd, this.updateForm.value).subscribe(async (resp: Response) => {
       
       this.utils.showSuccess("Atualização efetuada com sucesso", null);
       this.isSubmitted = false;
       this.createForm();
       this.dialogRef.close({event: this.action, confirm: true});

     }, async (err) => { 
       this.utils.showError("Não foi possível realizar a atualização do produto", err.message);
       this.closeModal();
     } );
    } 

     return;
 }

 preview(event) {
      
  this.url = null;
  this.updateForm.get('StrImag').setValue(null);
  let size = event.target.files[0].size;
  if(size <= 5240000) {//5mb 

    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
          this.url = event.target.result;
          this.updateForm.get('StrImag').setValue(btoa(reader.result.toString()))
      }
      this.imgSize = false;
      reader.readAsDataURL(event.target.files[0]);
    }
  }
  else {
    this.imgSize = true;
    this.updateForm.get('File').setValue(null);
  }
}


  closeModal(){
    this.dialogRef.close();
  }
  
  onFileChange(event){
    let fileList: FileList = event.target.files;
    if(fileList.length > 0) {
        let file = fileList[0];
    }
  }

  ngOnInit(): void {
    this.createForm();
    this.isSubmitted = false;
  }
 

}
