import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment.development";
import {HttpClient} from "@angular/common/http";
import {Site} from "../_model/site";
import { AuthenticationService } from './authentication.service';
import { Dashboard } from '../_model/dashboard';

@Injectable({
  providedIn: 'root'
})
export class SiteService {
  baseUrl: string = environment.baseApi

  constructor(private http: HttpClient, private authService: AuthenticationService) { }

  getSiteExcept(id: number){
    return this.http.get<Site[]>(this.baseUrl+ "/Site/except/" + id)
  }


  getDashboard() {
    return this.http.get<Dashboard>(this.baseUrl + "/Site/Dashboard/" + this.authService.site().id)
  }


}
