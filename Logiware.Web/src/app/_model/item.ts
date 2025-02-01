import { Site } from "./site";
import { ItemHistory } from "./itemHistory";
import { ShipmentItem } from "./shipmentItem";

export interface Item {
  id: number,
  name: string;
  description: string;
  category: string;
  quantity: number;
  shipmentItems: ShipmentItem[];
  itemCode: string;
}
