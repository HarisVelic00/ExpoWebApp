import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LocationCreationVM } from 'src/app/models/LocationCreationVM';
import { LocationVM } from 'src/app/models/LocationVM';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  URL: string = 'https://localhost:44337/Location';
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private httpClient: HttpClient) {}

  AddLocation(location: LocationCreationVM) {
    return this.httpClient.post(
      this.URL + '/AddLocation',
      location,
      this.options
    );
  }

  UpdateLocation(location: LocationVM) {
    return this.httpClient.put(
      this.URL + '/UpdateLocation/' + location.id,
      location,
      this.options
    );
  }
}
