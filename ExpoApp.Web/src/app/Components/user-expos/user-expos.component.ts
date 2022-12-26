import { Component, OnInit } from '@angular/core';
import { ExpoVM } from 'src/app/models/ExpoVM';
import { ExpoService } from 'src/app/services/Expo/Expo.service';

@Component({
  selector: 'app-user-expos',
  templateUrl: './user-expos.component.html',
  styleUrls: ['./user-expos.component.css']
})
export class UserExposComponent implements OnInit {

  dataSource: ExpoVM[] = [];

  constructor(private expoService: ExpoService) { }

  ngOnInit(): void {
    this.initDataSource();
  }

  initDataSource(){
    this.expoService.GetUserExpos().subscribe((res : any) =>{
      if (res.isSuccess) {
        this.dataSource = res.data;
      }
    });
  }

  displayedColumns: string[] = [
    'title',
    'opening',
    'closing',
    'location',
    'action',
  ];

  // dataSource: ExpoVM[] = [
  //   {
  //     id:1,
  //     title: 'Dino',
  //     description: 'Behrem',
  //     dateOfClosing: new Date(),
  //     dateOfOpening: new Date(),
  //     workHoursClosing: 4,
  //     workHoursOpening: 5,
  //     location: new LocationVM(),
  //     organiser: 'Dino',
  //     tickets: []
  //   },
  //   {
  //     id:2,
  //     title: 'Dino',
  //     description: 'Behrem',
  //     dateOfClosing: new Date(),
  //     dateOfOpening: new Date(),
  //     workHoursClosing: 4,
  //     workHoursOpening: 5,
  //     location: new LocationVM(),
  //     organiser: 'Dino',
  //     tickets: []
  //   },
  //   {
  //     id:3,
  //     title: 'Dino',
  //     description: 'Behrem',
  //     dateOfClosing: new Date(),
  //     dateOfOpening: new Date(),
  //     workHoursClosing: 4,
  //     workHoursOpening: 5,
  //     location: new LocationVM(),
  //     organiser: 'Dino',
  //     tickets: []
  //   },
  //   {
  //     id:4,
  //     title: 'Dino',
  //     description: 'Behrem',
  //     dateOfClosing: new Date(),
  //     dateOfOpening: new Date(),
  //     workHoursClosing: 4,
  //     workHoursOpening: 5,
  //     location: new LocationVM(),
  //     organiser: 'Dino',
  //     tickets: []
  //   },
  //   {
  //     id:5,
  //     title: 'Dino',
  //     description: 'Behrem',
  //     dateOfClosing: new Date(),
  //     dateOfOpening: new Date(),
  //     workHoursClosing: 4,
  //     workHoursOpening: 5,
  //     location: new LocationVM(),
  //     organiser: 'Dino',
  //     tickets: []
  //   },
  // ];
}
