import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PorukaUpdateComponent } from '../../poruke/poruka-update/poruka-update.component';

@Component({
  selector: 'app-edit-expos',
  templateUrl: './edit-expos.component.html',
  styleUrls: ['./edit-expos.component.css']
})

export class EditExposComponent implements OnInit {
  @Input() odaberiSajam: any;
  trajanje = 3;
  expoUrl: string = 'https://localhost:44337/Expo';

  constructor(private httpClient: HttpClient, private porukaUpdate: MatSnackBar) { }

  ngOnInit(): void { }


  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  spremiPromjene(){
    this.httpClient.put(this.expoUrl + "/UpdateExpoAdmin/" + this.odaberiSajam.id, this.odaberiSajam, this.options).subscribe((res: any) => {
    })
    this.porukaUpdate.openFromComponent(PorukaUpdateComponent, {
      duration: this.trajanje * 1000,
    })
    this.odaberiSajam.show = false;
  }


}
