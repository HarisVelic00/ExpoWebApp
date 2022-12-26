import {
  Component,
  Input,
  OnInit,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { ExpoSearchFilterVM } from 'src/app/models/ExpoSearchFilterVM';
import { ExpoVM } from 'src/app/models/ExpoVM';
import { ExpoService } from 'src/app/services/Expo/Expo.service';
@Component({
  selector: 'app-expo-card',
  templateUrl: './expo-card.component.html',
  styleUrls: ['./expo-card.component.css'],
})
export class ExpoCardComponent implements OnInit, OnChanges {
  expos: ExpoVM[] = [];
  @Input() filters? = new ExpoSearchFilterVM();

  constructor(private expoService: ExpoService) {}

  ngOnChanges() {
    this.expoService
      .GetExpos(this.filters)
      .subscribe((res: any) => (this.expos = res.data));
  }

  ngOnInit(): void {
    this.loadExpos();
  }

  loadExpos() {
    this.expoService
      .GetExpos(this.filters)
      .subscribe((res: any) => (this.expos = res.data));
  }
}
