import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { Notification } from '../_model/notification';
import { AuthenticationService } from './authentication.service';
import { environment } from '../../environments/environment.development';
import { toast } from 'ngx-sonner';
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  hubUrl = environment.hubUrl;
  hubConnection?: HubConnection
  constructor(private authService: AuthenticationService) { }


  createNotificationHub() {
    const token = this.authService.token();
    if (!token) return;
    this.hubConnection = new HubConnectionBuilder().withUrl(this.hubUrl + "/notifications", {
      accessTokenFactory: ()=> token,
    }).withAutomaticReconnect().build();
    this.hubConnection.start().then(() =>console.log("connection started successfully")).catch((err) => console.log(err));
  }

  listeningToNotification() {
    this.hubConnection!.on('ReceiveNotification', (notification: Notification) => {
      console.log('Received notification: ', notification);
      toast.info('Received notification: ', {
        description: notification.message,
        position: 'top-center',

      })
    });
  }


  sendNotification(notification: Notification) {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendNotification', notification).catch((err)=>console.log(err));
    }
  }

}
