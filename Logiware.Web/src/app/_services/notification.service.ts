import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Notification } from '../_model/notification';
import { AuthenticationService } from './authentication.service';
import { environment } from '../../environments/environment.development';
import { toast } from 'ngx-sonner';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  hubUrl = environment.hubUrl;
  hubConnection?: HubConnection;
  constructor(private authService: AuthenticationService) {}

  createNotificationHub() {
    const token = this.authService.token();
    if (!token) return;
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + "/notifications", {
        accessTokenFactory: () => token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log("Connection started successfully");
        this.listeningToNotification(); // Ensure this method is called after the connection is established
      })
      .catch((err) => console.log("Error starting connection: ", err));
  }

  listeningToNotification() {
    console.log("hub is still ACTIVE")
    console.log(this.hubConnection)
    if (this.hubConnection) {
      console.log("now listening")
      this.hubConnection.on('ReceiveNotification', (notification: Notification) => {
        console.log('Received notification: ', notification);
        toast.info('Received notification: ', {
          description: notification.message,
          position: 'top-center',
        });
      });
    } else {
      console.log("Hub connection is not established");
    }
  }

  sendNotification(notification: Notification) {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendNotification', notification)
        .catch((err) => console.log("Error sending notification: ", err));
    }
  }
}
