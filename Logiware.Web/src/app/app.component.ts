import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import {AuthenticationService} from "./_services/authentication.service";
import {User} from "./_model/user";
import {Authenticated} from "./_model/authenticated";
import {HlmToasterComponent} from "@spartan-ng/ui-helm-helm";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from "ngx-spinner";
import { NotificationService } from './_services/notification.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HlmToasterComponent, NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent  implements OnInit{

   title = 'Logiware.Web';
   constructor(private authService: AuthenticationService, private notificationService: NotificationService) {
   }
  ngOnInit(): void {
    this.setCurrentUser()
    this.notificationService.createNotificationHub()

    this.notificationService.listeningToNotification()
  }

  setCurrentUser(){
    const userString = localStorage.getItem('auth');

    if (!userString) {
      return;
    }
    const user: Authenticated = JSON.parse(userString);
    this.authService.setCurrentUser(user);
  }
}



