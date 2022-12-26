import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterVM } from 'src/app/models/RegisterVM';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm = new FormGroup({
    companyName: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  constructor(
    private loginService: LoginService,
    private formbuilder: FormBuilder,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.registerForm = this.formbuilder.group({
      companyName: ['', { validators: [Validators.required] }],
      email: ['', { validators: [Validators.required, Validators.email] }],
      password: ['', { validators: [Validators.required] }],
    });
  }
  onSubmit() {
    return this.loginService
      .SignUp(this.registerForm.value as RegisterVM)
      .subscribe((res: any) => {
        this.router.navigateByUrl('login');
      });
  }
}
