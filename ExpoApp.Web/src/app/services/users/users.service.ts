import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UsersVM } from '../../models/UsersVM';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private httpClient : HttpClient) { }

  usersURL: string = 'https://localhost:44337/User';

  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  GetUsers(){
    return this.httpClient.get<UsersVM[]>(this.usersURL+'/GetAllUsers',this.options);
  }


  DeleteUsers(user : UsersVM){
    return this.httpClient.delete(this.usersURL+'/DeleteUser/'+user.id, this.options);
  }

}
