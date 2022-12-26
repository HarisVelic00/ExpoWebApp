import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ExpoOrganizeVM } from 'src/app/models/ExpoOrganizeVM';
import { ExpoVM } from 'src/app/models/ExpoVM';
import { IndustryVM } from 'src/app/models/IndustryVM';
import { TicketVM } from 'src/app/models/TicketVM';
import { TicketTypeCreationVM } from 'src/app/models/TicketTypeCreationVM';
import { ExpoService } from 'src/app/services/Expo/Expo.service';
import { IndustryService } from '../../services/industry/industry.service';
import { LoginService } from 'src/app/services/login/login.service';
import { TicketTypeService } from 'src/app/services/TicketType/ticket-type.service';
import { LocationService } from 'src/app/services/location/location.service';
import { LocationVM } from 'src/app/models/LocationVM';
import { LocationCreationVM } from 'src/app/models/LocationCreationVM';

@Component({
  selector: 'app-update-expo',
  templateUrl: './update-expo.component.html',
  styleUrls: ['./update-expo.component.css']
})
export class UpdateExpoComponent implements OnInit, OnDestroy {

  expoDetails!: FormGroup;
  ticketForm!: FormGroup;
  ticketTypeForm: FormGroup = this.newTicketTypeForm();
  locationForm!: FormGroup;
  homePanel: boolean = true;
  expo!: ExpoVM;
  sub: any;
  id: number = 0;

  industries: IndustryVM[] = [];
  expoCover: any = File;
  imageURL: string = "";

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private expoService: ExpoService,
              private _industryService: IndustryService,
              private ticketTypeService: TicketTypeService,
              private locationService: LocationService,
              private loginService: LoginService) {}

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe((param : any) => {
      this.id = +param["id"];
      this.loadExpo();
    });

    this.newTicketTypeForm();
    this.initTiceketForm();
    //this.loadIndustries();
  }

  onDetailsSubmit() {
    console.log(this.expoDetails.value as ExpoOrganizeVM);
    this.expoDetails.addControl("Organizer", new FormControl(this.loginService.GetOrganiser()));
    this.expoService.UpdateExpoDetails(this.id, this.expoDetails.value as ExpoOrganizeVM).subscribe((res : any) => {
        alert(res.message);
    });
  }

  onLocationSubmit(){
    if(this.expo.location == null){
      this.expoService.AddLocation(this.locationForm.value as LocationCreationVM).subscribe((res : any) => {
        this.expo.location = res;
      });
    }else{
      this.expoService.UpdateLocation(this.locationForm.value as LocationVM).subscribe((res : any) => {
        this.expo.location = res;
      });
    }
  }

  //#region Init

  loadExpo() {
    this.expoService.GetExpoById(this.id).subscribe((res: any) => {
      if (res.isSuccess) {
        this.expo = res.data;
        this.initExpoForm();
        this.initLocationForm();
        this.initTiceketForm();
      }
    });
  }

  loadIndustries(){
     this._industryService.GetIndustry().subscribe((res:any)=>{
      this.industries=res;
    })
  }

  initExpoForm() {
    this.expoDetails = this.formBuilder.group({
      title: [this.expo?.title, { validators: Validators.required }],
      description: [this.expo?.description],
      dateOfOpening: [this.expo?.dateOfOpening, { validators: Validators.required }],
      dateOfClosing: [this.expo?.dateOfClosing, { validators: Validators.required }],
      workHoursOpening: [this.expo?.workHoursOpening, { validators: Validators.required }],
      workHoursClosing: [this.expo?.workHoursClosing, { validators: Validators.required }],
      industryId: [this.expo?.industry?.id, { validators: Validators.required}]
    });
  }

  initLocationForm(){
    this.locationForm =  this.formBuilder.group({
      id: new FormControl(this.expo.location?.id),
      adress: [this.expo?.location?.adress, { validators: Validators.required }],
      city: [this.expo?.location?.city, { validators: Validators.required }],
      zipCode: [this.expo?.location?.zipCode, { validators: Validators.required }],
      country: [this.expo?.location?.country, { validators: Validators.required }],
      expoId: new FormControl(this.id)
    });
  }

  initTiceketForm(){
    this.ticketForm = this.formBuilder.group({
      tickets: this.toFormArray()
    });
  }

  newTicketTypeForm(){
    return new FormGroup({
      id: new FormControl(0),
      expoId: new FormControl(this.id),
      name: new FormControl("", { validators: Validators.required}),
      price: new FormControl(0, { validators: Validators.required }),
      validDaysCount: new FormControl(1, { validators: [Validators.required, Validators.min(1)]})
    }, {updateOn: 'blur'});
  }

  //#endregion Init

  //#region Ticket management
  addTicektType(){
    this.getTickets().push(this.newTicketTypeForm());
  }

  removeTicketType(ticket: AbstractControl, index: number){
    this.ticketTypeService.DeleteTicketType(ticket.value as TicketVM).subscribe((res : any) => {
      if(res.isSuccess){
        this.getTickets().removeAt(index);
      }
    });
  }

  onTicketsSubmit(){
    console.log(this.ticketForm.value);
  }

  toFormArray(): FormArray {
    let tickets: FormArray = this.formBuilder.array([]);

    this.expo?.ticketTypes.forEach((ticket) => tickets.push(new FormGroup({
      id: new FormControl(ticket.id),
      expoId: new FormControl(this.id),
      name: new FormControl(ticket.name, { validators: Validators.required }),
      price: new FormControl(ticket.price, { validators: Validators.required }),
      validDaysCount: new FormControl(ticket.validDaysCount, { validators: [Validators.required, Validators.min(1)]})
    })));

    return tickets;
  }

  addTicket(ticket: AbstractControl)  {
    this.ticketTypeService.CreateTicketType(ticket.value as TicketTypeCreationVM).subscribe((res : any) => {
      alert(res.message);
    });
  }

  editTicket(ticket: AbstractControl)  {
    this.ticketTypeService.EditTicketType(ticket.value as TicketTypeCreationVM).subscribe((res : any) => {
      alert(res.message);
    });
  }
  //#endregion Ticket management

  //#region Image management
  openFileDialog(){
    let element = document.getElementById("upload-btn");
    element?.click();
  }

  uploadImage(image: any){
    if (image?.target?.files.length > 0) {
      this.expoCover = image?.target?.files[0];

      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.imageURL = event.target.result;
      };
      reader.readAsDataURL(this.expoCover);
    }
  }
  //#endregion Image management

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

  public get getAddress() {
    return this.expoDetails.get('location.address');
  }

  public get getCity() {
    return this.expoDetails.get('location.city');
  }

  public get getPostalCode() {
    return this.expoDetails.get('location.postalCode');
  }

  public get getCountry() {
    return this.expoDetails.get('location.country');
  }

  public getTickets() : FormArray {
    return this.ticketForm.get("tickets") as FormArray;
  }

  public get getTicketForm(): FormGroup {
    return this.ticketForm.get("ticketType") as FormGroup;
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

  clearAddress() {
    this.getAddress?.reset();
  }

  clearCity() {
    this.getCity?.reset();
  }

  clearPostalCode() {
    this.getPostalCode?.reset();
  }

  clearCountry() {
    this.getCountry?.reset();
  }

  //#endregion Clear inputs
}
