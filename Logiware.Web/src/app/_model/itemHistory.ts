import {Item} from "./item";
import {Site} from "./site";

export interface ItemHistory {
  id: number;
  item: Item;
  site: Site;
  remarks: string;
  status: 'Received' |  'Shipped' | 'Cancelled' | 'Partial' ;
  createdAt: Date
}
