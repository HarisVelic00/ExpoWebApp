import { Injectable } from '@angular/core';
import { LoginVM } from 'src/app/models/LoginVM';
import { RegisterVM } from 'src/app/models/RegisterVM';
import { AuthResponse } from 'src/app/models/AuthResponse';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  url: string = 'https://localhost:44337/Auth';
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient, private router: Router) {}

  isAuthenticated() {
    const token = this.GetToken();

    if (token === '') {
      return false;
    }

    const expiriation = this.GetExpiriation();
    const expiriationDate = new Date(expiriation as string);

    if (expiriationDate <= new Date()) {
      this.SignOut();
      return false;
    }

    return true;
  }

  SignUp(register: RegisterVM): any {
    return this.http.post<RegisterVM>(
      this.url + '/Register',
      register,
      this.options
    );
  }

  SignIn(login: LoginVM): any {
    return this.http.post(this.url + '/Login', login, this.options);
  }

  SignOut() {
    const tokenKey: string = 'token';
    const expiriation: string = 'token-expiriation';
    localStorage.removeItem(tokenKey);
    localStorage.removeItem(expiriation);
  }

  SaveToken(authResponse: AuthResponse) {
    const tokenKey: string = 'token';
    const expiriation: string = 'token-expiriation';
    localStorage.setItem(tokenKey, authResponse.token);
    localStorage.setItem(expiriation, authResponse.expiriation.toString());
  }

  GetToken() {
    const tokenKey: string = 'token';
    return localStorage.getItem(tokenKey);
  }

  GetExpiriation() {
    const expiriation: string = 'token-expiriation';
    return localStorage.getItem(expiriation);
  }

  GetJWTData(prop: string) {
    const tokenKey: string = 'token';
    const token = localStorage.getItem(tokenKey);

    if (!token) {
      return '';
    }

    const dataProps = token.split('.');
    const dataValue = JSON.parse(atob(dataProps[1]));

    return dataValue[prop];
  }

  GetUser() {
    if (!this.isAuthenticated()) {
      this.router.navigateByUrl('login');
    }
    const prop = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
    const username = this.GetJWTData(prop);
    if (!username) {
      this.router.navigateByUrl('login');
    }
    return username;
  }

  GetOrganiser() {
    if (!this.isAuthenticated()) {
      alert('You are not logged in!');
      return '';
    }
    const prop = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
    const userRole = 'role';
    const username = this.GetJWTData(prop);
    const role = this.GetJWTData(userRole);
    if (!username) {
      return '';
    }
    if (role !== 'Organizer') {
      return '';
    }
    return username;
  }
}
