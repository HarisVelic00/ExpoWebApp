import { Component, OnDestroy, OnInit } from '@angular/core';
import { LoginService } from 'src/app/services/login/login.service';
import { NotificationService } from 'src/app/services/notification.service';
import * as signalR from '@microsoft/signalr';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  constructor(
    private notificationService: NotificationService,
    private loginService: LoginService,
    private router: Router
  ) {}

  notificationCount: number = 0;
  connection!: signalR.HubConnection;
  notificationSubscription!: Subscription;

  ngOnInit(): void {
    this.loadNotificationsCount();
  }

  ngOnDestroy(): void {
    console.log('Conenction stoped!');
    this.connection.stop();
    this.notificationSubscription.unsubscribe();
  }

  loadNotificationsCount() {
    this.notificationSubscription = this.notificationService
      .getUnreadCount()
      .subscribe((res: any) => {
        this.notificationCount = res;
      });

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.notificationService.URL + '/notify')
      .build();

    this.connection
      .start()
      .then(function () {
        console.log('SignalR Connected!');
      })
      .catch(function (err) {
        return console.error('error' + err.toString());
      });

    this.connection.on('BroadcastMessage', () => {
      this.notificationService.getUnreadCount().subscribe((res: any) => {
        this.notificationCount = res;
      });
    });
  }

  signOut() {
    this.loginService.SignOut();
    this.router.navigateByUrl('login');

    this;
  }
}
