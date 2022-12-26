import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PorukaUpdateComponent } from '../../poruke/poruka-update/poruka-update.component';

@Component({
  selector: 'app-edit-industry',
  templateUrl: './edit-industry.component.html',
  styleUrls: ['./edit-industry.component.css']
})
export class EditIndustryComponent implements OnInit {
  constructor(private httpClient: HttpClient, private porukaUpdate: MatSnackBar) { }
  ngOnInit(): void { }
  @Input() odaberiIndustriju: any;
  trajanje = 3;
  industryURL: string = 'https://localhost:44337/Industry';

  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  spasiPromjene() {
    this.httpClient.put(this.industryURL + "/UpdateIndustry/" + this.odaberiIndustriju.id, this.odaberiIndustriju, this.options).subscribe((res: any) => {
    })
    this.porukaUpdate.openFromComponent(PorukaUpdateComponent, {
      duration: this.trajanje * 1000,
    })
    this.odaberiIndustriju.show = false;
  }

}
