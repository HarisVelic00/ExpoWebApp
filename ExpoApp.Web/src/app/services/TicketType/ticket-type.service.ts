import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TicketTypeCreationVM } from 'src/app/models/TicketTypeCreationVM';
import { TicketVM } from 'src/app/models/TicketVM';

@Injectable({
  providedIn: 'root',
})
export class TicketTypeService {
  constructor(private httpClinet: HttpClient) {}

  Url: string = 'https://localhost:44337/TicketType';
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  CreateTicketType(input: TicketTypeCreationVM) {
    return this.httpClinet.post(
      this.Url + '/CreateTicketType',
      input,
      this.options
    );
  }

  EditTicketType(input: TicketTypeCreationVM) {
    return this.httpClinet.put(
      this.Url + '/UpdateTicketType',
      input,
      this.options
    );
  }

  DeleteTicketType(input: TicketVM) {
    return this.httpClinet.delete(
      this.Url + '/DeleteTicketType/' + input.id,
      this.options
    );
  }

  GetTicketTypes(expoId: number) {
    return this.httpClinet.get(
      this.Url + '/GetTicketTypes/' + expoId,
      this.options
    );
  }
}
