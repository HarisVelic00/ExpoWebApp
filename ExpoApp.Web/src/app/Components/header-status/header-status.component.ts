import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-header-status',
  templateUrl: './header-status.component.html',
  styleUrls: ['./header-status.component.css']
})
export class HeaderStatusComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  isLogged() {
    return this.loginService.isAuthenticated();
  }
}
