import {Personnel} from "./personnel";
import {Site} from "./site";
import {ShipmentItem} from "./shipmentItem";
import {Item} from "./item";
import {Ownership} from "./ownership";

export interface Shipment {
  id? : number;
  shipmentDate: string;
  shipmentCode: string;
  authorizedBy: Personnel;
  driver: Personnel;
  site: Site;
  destinationSite?: Site;
  status: 'Received' |  'Shipped' | 'Cancelled' | 'Partial' ;
  statusUpdate: string;
  shipmentItems: ShipmentItem[]
}


export interface CreateShipment{

  driverId?: number;
  authorizedBy?: number;
  siteId?: number;
  destinationSiteId?: number;
  shipmentItem: ShipmentItem[]
}


export interface ShipmentDirection{
  siteId: number;
  direction: 'INBOUND' | 'OUTBOUND'
}




