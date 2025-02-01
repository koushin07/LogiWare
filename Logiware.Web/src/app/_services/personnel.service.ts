import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment.development";
import { HttpClient } from "@angular/common/http";
import { CreatePersonnel, Personnel } from "../_model/personnel";

@Injectable({
  providedIn: 'root'
})
export class PersonnelService {
  baseUrl: string = environment.baseApi

  constructor(private http: HttpClient) { }

  createPersonnel(personnel: CreatePersonnel) {
    return this.http.post<Personnel>(this.baseUrl + "/Personnel", personnel)
  }
  getDriverPersonnel() {
    return this.http.get<Personnel[]>(this.baseUrl + "/Personnel/driver")
  }

  getManagerPersonnel() {
    return this.http.get<Personnel[]>(this.baseUrl + "/Personnel/manager")
  }

  getAuthorizePersonnel(code: string) {
    return this.http.get<Personnel>(this.baseUrl + "/Personnel/authorize/" + code)
  }
  getPersonnelBySite(siteId: number) {
    return this.http.get<Personnel[]>("https://localhost:5001/api/Personnel/site/get/"+siteId)
  }
}
