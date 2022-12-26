import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ExpoOrganizeVM } from 'src/app/models/ExpoOrganizeVM';
import { IndustryVM } from 'src/app/models/IndustryVM';
import { ExpoService } from 'src/app/services/Expo/Expo.service';
import { LoginService } from 'src/app/services/login/login.service';
import { IndustryService } from '../../services/industry/industry.service';


@Component({
  selector: 'app-expo-create',
  templateUrl: './expo-create.component.html',
  styleUrls: ['./expo-create.component.css'],
})
export class ExpoCreateComponent implements OnInit, OnDestroy {
  expoDetails!: FormGroup;
  homePanel: boolean = true;
  industries: IndustryVM[] = [];
  timeOut: any;

  constructor(private formBuilder: FormBuilder,
              private expoService: ExpoService,
              private _industryService: IndustryService,
              private loginService : LoginService,
              private _router: Router) {}

  ngOnDestroy(): void {
    clearTimeout(this.timeOut);
  }

  ngOnInit(): void {
    this.initExpoForm();
    this.loadIndustries();
  }

  initExpoForm() {
    this.expoDetails = this.formBuilder.group({
      title: [null, { validators: Validators.required }],
      description: [null],
      dateOfOpening: [null, { validators: Validators.required }],
      dateOfClosing: [null, { validators: Validators.required }],
      workHoursOpening: [null, { validators: Validators.required }],
      workHoursClosing: [null, { validators: Validators.required }],
      industryId: [null, { validators: Validators.required}]
    });
  }

  loadIndustries(){
    this._industryService.GetIndustry().subscribe((res:any)=>{
      this.industries=res;
    })
  }

  onSubmit() {
    let username = this.loginService.GetOrganiser();
    this.expoDetails.addControl("organizer", new FormControl(username));
    console.log(this.expoDetails.value as ExpoOrganizeVM);

    this.expoService.AddExpo(this.expoDetails.value as ExpoOrganizeVM).subscribe((res:any) => {
      if (res.isSuccess) {
        this.timeOut = setTimeout(()=> {
          this._router.navigate(["update", res.data.id]);
        }, 2000);
      }
    });
  }


  //#region Getters

  public get getTitle() {
    return this.expoDetails.get('title');
  }

  public get getDateOfOpening() {
    return this.expoDetails.get('dateOfOpening');
  }

  public get getDateOfClosing() {
    return this.expoDetails.get('dateOfClosing');
  }
  //#endregion Getters

  //#region Clear inputs

  clearTitle() {
    this.getTitle?.reset();
  }

  clearDateOfOpening() {
    this.getDateOfOpening?.reset();
  }

  clearDateOfClosing() {
    this.getDateOfClosing?.reset();
  }

  //#endregion Clear inputs
}
