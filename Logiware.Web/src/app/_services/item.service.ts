import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment.development";
import {HttpClient} from "@angular/common/http";
import {Item} from "../_model/item";
import {take} from "rxjs";
import {FormGroup} from "@angular/forms";
import {Ownership} from "../_model/ownership";

@Injectable({
  providedIn: 'root'
})
export class ItemService {
  baseUrl: string= environment.baseApi

  constructor(private http: HttpClient) { }


  getItem(siteId: number){
    return this.http.get<Item[]>(this.baseUrl + "/Item/"+ siteId).pipe(take(1))
  }

  getItemById(id: number, siteId: number){
    return this.http.get<Ownership>(this.baseUrl + '/Ownership/item/' + id + '/site/' + siteId).pipe(take(1))
  }

  createItem(item: FormGroup){
    return this.http.post<Item>(this.baseUrl+ "/Item", item.value).pipe(take(1))
  }

  updateItem(item: Item){
    return this.http.put(this.baseUrl + "/Item", item).pipe(take(1))
  }

  getOwner(siteId: number){
    return this.http.get<Ownership[]>(this.baseUrl + "/Ownership/site/"+ siteId).pipe(take(1))
  }

}
