import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { IndustryVM } from '../../models/IndustryVM';

@Injectable({
  providedIn: 'root'
})
export class IndustryService {

  constructor(private httpClient : HttpClient){}

  industryURL: string = 'https://localhost:44337/Industry';

  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  GetIndustry(){
    return this.httpClient.get<IndustryVM[]>(this.industryURL+'/GetIndustries',this.options);
  }

  DeleteIndustry(industry: IndustryVM){
      return this.httpClient.delete(this.industryURL+'/DeleteIndustries/'+industry.id,this.options);
  }

  UpdateIndustry(industry: IndustryVM){
    return this.httpClient.put(this.industryURL+'/UpdateIndustry/'+industry.id,industry,this.options);
  }

  /*getndustries():Observable<IndustryVM[]>
  {
    return this.httpClient.get<IndustryVM[]>(this.industryURL);
  }*/

}
