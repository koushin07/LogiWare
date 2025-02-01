import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment.development";
import {ReceiveShipment} from "../_model/shipmentReceive";

@Injectable({
  providedIn: 'root'
})
export class ShipmentReceiveService {
  baseUrl = environment.baseApi
  constructor(private http: HttpClient) { }


  receiveItemShip(model: ReceiveShipment){
    return this.http.post(this.baseUrl+ '/ShipmentReceive/receive-item', model)
  }
}

