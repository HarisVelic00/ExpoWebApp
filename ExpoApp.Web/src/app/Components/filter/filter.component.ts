import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
} from '@angular/forms';
import { ExpoSearchFilterVM } from 'src/app/models/ExpoSearchFilterVM';
import { IndustryVM } from 'src/app/models/IndustryVM';
import { IndustryService } from '../../services/industry/industry.service';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.css'],
})
export class FilterComponent implements OnInit {
  industries!: IndustryVM[];
  _filterForm!: FormGroup;

  @Output() filterEmiter = new EventEmitter<ExpoSearchFilterVM>();

  constructor(
    private _fromBuilder: FormBuilder,
    private _industryService: IndustryService
  ) {}

  ngOnInit(): void {
    this.initFilterForm();
    this.loadIndustries();
  }

  initFilterForm() {
    this._filterForm = this._fromBuilder.group({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      industry: new FormControl(),
      priceFrom: new FormControl(),
      priceTo: new FormControl(),
      title: new FormControl(),
    });
  }

  loadIndustries() {
   this._industryService.GetIndustry().subscribe((res:any)=>{
    this.industries=res;
   });
  }

  filterExpos() {
    this.filterEmiter.emit(this._filterForm.value as ExpoSearchFilterVM);
  }

  clearFilters() {
    this._filterForm.reset();
    this.filterExpos();
  }

  isEmpty(control: AbstractControl | null): boolean {
    return control?.value == '';
  }

  //#region Getters

  public get getTitle() {
    return this._filterForm.get('title');
  }

  public get getPriceFrom() {
    return this._filterForm.get('priceFrom');
  }

  public get getPriceTo() {
    return this._filterForm.get('priceTo');
  }

  public get getDateFrom() {
    return this._filterForm.get('dateFrom');
  }

  public get getDateTo() {
    return this._filterForm.get('dateTo');
  }

  //#endregion Getters

  //#region Clear form region

  clearTitle() {
    this.getTitle?.reset();
  }

  clearPriceFrom() {
    this.getPriceFrom?.reset();
  }

  clearPriceTo() {
    this.getPriceTo?.reset();
  }

  clearDateFrom() {
    this.getDateFrom?.reset();
  }

  clearDateTo() {
    this.getDateTo?.reset();
  }

  //#endregion Clear form region
}
