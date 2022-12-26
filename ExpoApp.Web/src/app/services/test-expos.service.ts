import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Expos {
  url: string = 'https://run.mocky.io/v3/edaa3b5f-b600-4a42-9161-561079837f02';

  constructor(private httpClient: HttpClient) {}

  getPosts() {
    return this.httpClient.get(this.url);
  }
}
