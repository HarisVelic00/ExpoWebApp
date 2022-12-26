import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { NotificationVM } from '../models/NotificationVM';
import { LoginService } from './login/login.service';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  public URL: string = 'https://localhost:44337';
  username: string = this.loginService.GetUser();
  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  notifications: NotificationVM[] = [];

  constructor(
    private httpClient: HttpClient,
    private loginService: LoginService
  ) {}

  getNotifications() {
    return this.httpClient.get(
      this.URL + '/Notification/GetUserNotifications/' + this.username,
      this.options
    );
  }

  getUnreadCount() {
    return this.httpClient.get(
      this.URL + '/Notification/GetNotificationsCount/' + this.username,
      this.options
    );
  }

  setAsSeen(id: number) {
    return this.httpClient.put(
      this.URL + '/Notification/SetNotificationAsSeen/' + id,
      null,
      this.options
    );
  }
}
