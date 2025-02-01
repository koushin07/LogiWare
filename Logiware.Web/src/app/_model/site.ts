import {Personnel} from "./personnel";

export interface Site{
  id: number
  name: string,
  location: string,
  description: string,
  personnel: Personnel
}
