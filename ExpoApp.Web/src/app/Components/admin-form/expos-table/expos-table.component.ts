import { Component, OnInit } from '@angular/core';
import { ExpoVM } from '../../../models/ExpoVM';
import { ExpoService } from '../../../services/Expo/Expo.service';
import { PorukeComponent } from '../../poruke/poruke.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-expos-table',
  templateUrl: './expos-table.component.html',
  styleUrls: ['./expos-table.component.css']
})
export class ExposTableComponent implements OnInit {

  data: ExpoVM[] = [];
  trajanje = 2;
  expos: any;
  dataSource = new MatTableDataSource(this.data);

  displayedColumns: string[] = [
    'title',
    'dateOfOpening',
    'dateOfClosing',
    'organiser',
    'industry',
    'hasExpired',
    'action',
  ];

  constructor(private service: ExpoService, private porukaSucess: MatSnackBar) {
  }

  ngOnInit(): void {
    this.service.GetExpos().subscribe((x: any) => {
      this.data = x.data;
      console.log(this.data);
      this.dataSource = new MatTableDataSource(this.data);
    })
  }

  obrisiSajam(expo: ExpoVM) {
    this.service.DeleteExpo(expo).subscribe(() => {
      // this.data.forEach((value,index)=>{
      //   if(value.id==expo.id){
      //     this.data.splice(index,1);
      //   }
      // })
      this.service.GetExpos().subscribe((x: any) => {
        this.data = x.data;
        console.log(this.data);
      })
    })
    this.porukaSucess.openFromComponent(PorukeComponent, {
      duration: this.trajanje * 1000,
    })
  }

  odaberiSajam(sajam: ExpoVM) {
    this.expos = sajam;
    this.expos.show = true;
    console.log(this.expos);
  }

  filterData(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.data = this.data.filter(e => e.title.toLowerCase().includes(event.target.value.toLowerCase()))
  }

}
