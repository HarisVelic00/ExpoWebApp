import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PorukaUpdateComponent } from '../../poruke/poruka-update/poruka-update.component';

@Component({
  selector: 'app-edit-users',
  templateUrl: './edit-users.component.html',
  styleUrls: ['./edit-users.component.css']
})
export class EditUsersComponent implements OnInit {
  prikazi: any = null;
  trajanje = 3;
  @Input() odaberiKorisnika: any;
  usersURL: string = 'https://localhost:44337/User';

  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient, private porukaUpdate: MatSnackBar) { }

  ngOnInit(): void { }

  spasiPromjene() {
    this.httpClient.put(this.usersURL + "/UpdateUsers/" + this.odaberiKorisnika.id, this.odaberiKorisnika, this.options).subscribe((res: any) => {
    })
    this.porukaUpdate.openFromComponent(PorukaUpdateComponent, {
      duration: this.trajanje * 1000,
    })
    this.odaberiKorisnika.show = false;
  }

}
