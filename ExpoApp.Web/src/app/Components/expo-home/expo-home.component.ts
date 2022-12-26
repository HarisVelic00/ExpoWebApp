import { Component, OnInit } from '@angular/core';
import { ExpoSearchFilterVM } from 'src/app/models/ExpoSearchFilterVM';

@Component({
  selector: 'app-expo-home',
  templateUrl: './expo-home.component.html',
  styleUrls: ['./expo-home.component.css'],
})
export class ExpoHomeComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}

  expoFilters?: ExpoSearchFilterVM;

  filterExpos(filters: ExpoSearchFilterVM) {
    this.expoFilters = filters;
    console.log(this.expoFilters);
  }

  getFilters() {
    return this.expoFilters;
  }
}
