import { Component } from '@angular/core';
import { LoginService } from './services/login/login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'ExpoApp.Web';

  constructor(private loginService: LoginService) {}

  loggedUser() {
    return this.loginService.isAuthenticated();
  }
}
