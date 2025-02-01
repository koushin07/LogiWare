import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment.development";
import {HttpClient, HttpParams} from "@angular/common/http";
import {CreateShipment, Shipment, ShipmentDirection} from "../_model/shipment";
import {AuthenticationService} from "./authentication.service";
import {log} from "@angular-devkit/build-angular/src/builders/ssr-dev-server";
import {map, Observable, take} from "rxjs";
import {ShipmentItem} from "../_model/shipmentItem";
import { ShipmentByDate } from '../_model/dashboard';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ShipmentService {
  baseUrl: string = environment.baseApi

  constructor(private http: HttpClient, private authService: AuthenticationService, private datePipe: DatePipe) { }


  getAllShipment(){
    const site = this.authService.getUserSite();
    if (site  == null) console.log("no site in local storage");

    return this.http.get<Shipment[]>(this.baseUrl + "/Shipment/" + site?.id);
  }



  sendShipment(model: CreateShipment){
    console.log("this is hit")
    return this.http.post<Shipment>(this.baseUrl + "/Shipment", model)
  }

  getShipmentByCode(param: string) {
    return this.http.get<Shipment>(this.baseUrl+ "/Shipment/code/"+ param)
  }

  receiveItemShipped(model: ShipmentItem[]){
    return this.http.put(this.baseUrl+ "/Shipment/receive-item", model, { withCredentials: true })
  }

  getShipmentDirection(adjective: "INBOUND" | "OUTBOUND"){
    let params = new HttpParams().set("SiteId",this.authService.site().id).set("Adjective", adjective);

    return this.http.get<Shipment[]>(this.baseUrl + "/Shipment", { params })
  }

 getShipmentComparison(): Observable<ShipmentByDate[]> {
    return this.http.get<ShipmentByDate[]>(this.baseUrl + "/Shipment/in-out-bound/" + this.authService.site().id)
      .pipe(
        map((res: ShipmentByDate[]) => {
          res.forEach((shipment: ShipmentByDate) => {
            // Process each shipment here
            shipment.createdAt = this.formatDate(shipment.createdAt);
          });
          return res; // Return the modified array if needed
        })
      );
  }

    formatDate(dateString: string): string {
    // Convert the date string to a Date object
    const date = new Date(dateString);

    // Format the date to a string like "January"
    return this.datePipe.transform(date, 'MMM') || '';
  }


  cancelShipment(code: string) {
    return this.http.put(this.baseUrl + '/shipment/cancel/'+ code, {}, {withCredentials: true})
  }
  getStatusClass(status: Shipment['status']): string {
    switch (status) {
      case 'Received': return 'bg-green-100 text-green-800';
      case 'Shipped': return 'bg-blue-100 text-blue-800';
      case 'Cancelled': return 'bg-red-100 text-red-800';
      case 'Partial': return 'bg-yellow-100 text-yellow-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }
}
