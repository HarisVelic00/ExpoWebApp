import { Injectable } from '@angular/core';
import { ExpoOrganizeVM } from 'src/app/models/ExpoOrganizeVM';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { ExpoVM } from 'src/app/models/ExpoVM';
import { LoginService } from '../login/login.service';
import { LocationCreationVM } from 'src/app/models/LocationCreationVM';
import { LocationVM } from 'src/app/models/LocationVM';
import { ExpoSearchFilterVM } from 'src/app/models/ExpoSearchFilterVM';

@Injectable({
  providedIn: 'root',
})
export class ExpoService {
  expos: ExpoVM[] = [];

  Url: string = 'https://localhost:44337/Expo';
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient, private loginService: LoginService) {}

  GetExpos(filters?: ExpoSearchFilterVM): Observable<ExpoVM[]> {
    const httpParams = this.GenerateQueryParams(filters);

    return this.http.get<ExpoVM[]>(this.Url + '/GetAll', {
      headers: this.options.headers,
      params: httpParams,
    });
  }

  /*GetAllExpos(){
    return this.http.get<ExpoVM>(this.Url+'/GetAll',this.options);
  }*/

  AddExpo(ExpoOrganizeVM: ExpoOrganizeVM): Observable<any> {
    return this.http.post<ExpoOrganizeVM>(
      this.Url + '/AddExpo',
      ExpoOrganizeVM,
      this.options
    );
  }

  GetExpoById(id: number) {
    return this.http.get(this.Url + '/GetById/' + id, this.options);
  }

  UpdateExpoDetails(id: number, details: ExpoOrganizeVM) {
    return this.http.put<ExpoOrganizeVM>(
      this.Url + '/UpdateExpo/' + id,
      details,
      this.options
    );
  }

  GetUserExpos() {
    let username = this.loginService.GetOrganiser();
    return this.http.get(this.Url + '/GetUserExpos/' + username);
  }

  AddLocation(location: LocationCreationVM) {
    return this.http.post(
      this.Url + '/AddExpoLocation',
      location,
      this.options
    );
  }

  UpdateLocation(location: LocationVM) {
    return this.http.put(
      this.Url + '/UpdateExpoLocation/' + location.id,
      location,
      this.options
    );
  }

  CanEditExpo(username: string, expoId: number) {
    const httpParams: HttpParams = new HttpParams()
      .set('expoId', expoId)
      .set('username', username);

    return this.http.get(this.Url + '/CanEditExpo', { params: httpParams });
  }

  private GenerateQueryParams(queryFilter?: ExpoSearchFilterVM) {
    let queryParams: HttpParams = new HttpParams();
    if (queryFilter) {
      for (var [key, value] of Object.entries(queryFilter)) {
        if (value) {
          if (value instanceof Date) {
            value = value.toISOString();
          }
          queryParams = queryParams.append(key, value);
        }
      }
    }

    return queryParams;
  }


  DeleteExpo(expo : ExpoVM){
      return this.http.delete(this.Url+'/DeleteExpoAdmin/'+expo.id,this.options);
  }



}
