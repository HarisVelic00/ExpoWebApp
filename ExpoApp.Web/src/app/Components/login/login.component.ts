import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';
import { LoginVM } from 'src/app/models/LoginVM';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(
    private loginService: LoginService,
    private formbuilder: FormBuilder,
    private router: Router
  ) { }
  
  ngOnInit(): void {
    this.initLoginForm();
  }

  initLoginForm() {
    this.loginForm = this.formbuilder.group({
      username: ['', { validators: [Validators.required] }],
      password: ['', { validators: [Validators.required] }],
    });
  }

  onSubmit() {
    this.loginService
      .SignIn(this.loginForm.value as LoginVM)
      .subscribe((res: any) => {
        this.loginService.SaveToken(res);
        this.router.navigateByUrl('home');
      });
  }

  clicksub() {
    this.loginForm.reset();
  }
}
