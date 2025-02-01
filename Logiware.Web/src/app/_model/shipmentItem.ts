import {Ownership} from "./ownership";
import {ReceiveShipment, ShipmentReceive} from "./shipmentReceive";

export interface ShipmentItem{
  id: number;
  quantity: number | null;
  shipmentItemCode: string;
  ownership: Ownership;
  shipmentReceives: ShipmentReceive[]
  receive?: number | null;

}


