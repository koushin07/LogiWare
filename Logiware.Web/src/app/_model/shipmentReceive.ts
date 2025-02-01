import { Item } from "./item";
import { Personnel } from "./personnel";

export interface ReceiveShipment {
  driver: Personnel
  authorizedBy: Personnel
  shipmentCode: string;
  status: 'Received' |  'Shipped' | 'Cancelled' | 'Partial' ;
  shipmentReceives: CreateShipmentReceive[];
  siteId: number | null;
}

export interface CreateShipmentReceive{
  item: Item
  shipmentItemCode: string,
  MissingQuantity: number,
  ReceiveQuantity: number
}

export interface ShipmentReceive {
  id: number;
  quantityReceived: number
  quantityMissing: number
}
