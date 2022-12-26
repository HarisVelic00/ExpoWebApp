import { Component, OnDestroy, OnInit } from '@angular/core';
import { NotificationVM } from 'src/app/models/NotificationVM';
import { NotificationService } from 'src/app/services/notification.service';
import * as signalR from '@microsoft/signalr';
import { LoginService } from 'src/app/services/login/login.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css'],
})
export class NotificationsComponent implements OnInit, OnDestroy {
  notifications: NotificationVM[] = [];
  connection!: signalR.HubConnection;
  notificationSubscription!: Subscription;

  constructor(
    private notificationService: NotificationService,
    private loginService: LoginService
  ) {}

  ngOnInit(): void {
    if (this.loginService.isAuthenticated()) {
      this.loadNotifications();
    }
  }

  ngOnDestroy(): void {
    console.log('Conenction stoped!');
    this.connection.stop();
    this.notificationSubscription.unsubscribe();
  }

  loadNotifications() {
    this.notificationSubscription = this.notificationService
      .getNotifications()
      .subscribe((res: any) => {
        this.notifications = res;
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
      this.notificationService.getNotifications().subscribe((res: any) => {
        this.notifications = res;
      });
    });
  }

  markAsSeen(notification: NotificationVM) {
    if (!notification.isSeen) {
      this.notificationService.setAsSeen(notification.id).subscribe();
    }
  }
}
