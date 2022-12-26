import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-apply-form',
  templateUrl: './apply-form.component.html',
  styleUrls: ['./apply-form.component.css'],
})
export class ApplyFormComponent implements OnInit {
  applyForm = new FormGroup({
    fullname: new FormControl('', Validators.required),
    email: new FormControl('', Validators.required),
    mobile: new FormControl('', Validators.required),
  });

  constructor(private formbuilder: FormBuilder, private router: Router) {}
  ngOnInit(): void {
    this.applyForm = this.formbuilder.group({
      fullname: ['', { validators: [Validators.required] }],
      email: ['', { validators: [Validators.required] }],
      mobile: ['', { validators: [Validators.required] }],
    });
  }
  clicksub() {
    console.log(this.applyForm.value);
    this.applyForm.reset();
  }
}
