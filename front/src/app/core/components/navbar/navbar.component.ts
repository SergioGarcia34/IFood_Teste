import { Component, OnInit } from '@angular/core';
import { UtilsService } from '../../services/utils.service'

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(public utils : UtilsService) { }

  ngOnInit(): void {
  }

}
