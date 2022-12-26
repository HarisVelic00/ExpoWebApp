import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { OrganizerVM } from 'src/app/models/OrganizerVM';

@Injectable({
  providedIn: 'root',
})
export class OrganizerService {
  url: string = 'https://localhost:44337/User';
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient, private router: Router) {}

  GetAll(): Observable<OrganizerVM[]> {
    return this.http.get<OrganizerVM[]>(this.url + '/GetUsers', this.options);
  }

  IsOrganizer(username: string) {
    return this.http.get(this.url + '/IsOrganizer/' + username, this.options);
  }
}
