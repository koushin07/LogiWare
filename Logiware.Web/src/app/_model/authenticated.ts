import {User} from "./user";
import {Site} from "./site";

export interface Authenticated{
  id: number,
  user: User,
  token: string
}
