import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  constructor(private toastr : ToastrService) { }

  // toastr

  showError(message, title){
    this.toastr.error(message, title)      
  }
 
  showSuccess(message, title){
   this.toastr.success(message, title)      
  }


  // token

  setLocalStorage(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  getLocalStorage(key: string) {
      return localStorage.getItem(key);
  }

  deletelocalStorage(key: string) {
      localStorage.removeItem(key);
  }

}
