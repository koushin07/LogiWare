import {Site} from "./site";
import {Item} from "./item";
import {ItemHistory} from "./itemHistory";

export interface Ownership{
  id: number,
  quantity: number,
  site: Site,
  item: Item
  itemHistories: ItemHistory[];

}
