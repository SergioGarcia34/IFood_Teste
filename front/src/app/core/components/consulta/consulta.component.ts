import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../../services/produto.service'
import { UtilsService } from '../../services/utils.service'
import { Produto } from '../../shared/models/Produto.models';
import { MatDialog, MatDialogRef} from '@angular/material/dialog'
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-consulta',
  templateUrl: './consulta.component.html',
  styleUrls: ['./consulta.component.scss']
})

export class ConsultaComponent implements OnInit {
  
  public produtos : Produto [] = []

  private modal : MatDialogRef<ModalComponent, any>;
 
  constructor(public utils: UtilsService, private produto : ProdutoService, private dialog : MatDialog) { }

  ngOnInit(): void {

    this.getAllProdutos()

  }

  
  openModal(action, obj){
    
    obj.action = action;
   
    this.modal = this.dialog.open(ModalComponent, {
      width: 'auto',
      data: obj,
      disableClose: true
    })

    this.modal.afterClosed().subscribe(result => {
      
      if(result.confirm){
        this.produtos = [];
        this.getAllProdutos();
      }
    })
  }




 getAllProdutos(){

      this.produto.getProduto().subscribe(async (resp: any) => {

        this.produtos = resp;
       
      }, async (err) => { 
  
        this.utils.showError("Não foi possível realizar a consulta dos produtos", err.message);
        return;
      });

      return;
 }

}
