import {Injectable, signal} from '@angular/core';
import {environment} from "../../environments/environment.development";
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, map} from "rxjs";
import {User} from "../_model/user";
import {Authenticated} from "../_model/authenticated";
import {FormGroup} from "@angular/forms";
import {Site} from "../_model/site";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl: string= environment.baseApi
  private currentUserSource = new BehaviorSubject<Authenticated | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  site = signal<Site>({} as Site)
  user = signal<User | null>(null)
  readonly token = signal<string | null>(null);

  constructor(private http: HttpClient) {
    const storedSite = localStorage.getItem('site')
    if(storedSite){
      this.site.set(JSON.parse(storedSite))
    }

    const token = localStorage.getItem('token')
    if (token) {
      this.token.set(token)

    }
    const auth = localStorage.getItem('auth')

    if (auth) {
      this.currentUserSource.next(JSON.parse(auth))
    }
  }

  login(model: FormGroup){
    return this.http.post<Authenticated>(this.baseUrl+"/Authentication/login",model.value).pipe(
      map(res=>{
        console.log(res)
        this.currentUserSource.next(res);
        this.token.set(res.token);

        const user: User = res.user

        localStorage.setItem('user',JSON.stringify(user))
        localStorage.setItem('auth', JSON.stringify(res))
        localStorage.setItem("token",  this.addCharToToken(res.token))
        localStorage.setItem('site', JSON.stringify(res.user.site))
        this.site.set(res.user.site)
        this.user.set(user)
        this.token.set(res.token)
      })
    )
  }


  public setCurrentUser(auth: Authenticated){
    const token = localStorage.getItem('token')
    console.log(token)
    if(!token) return;
    auth.token =this.removeCharToToken(token)
    console.log(auth.token)
    this.currentUserSource.next(auth);
    this.site.set(auth.user.site)
    this.user.set(auth.user)
    this.token.set(auth.token)
  }


  private decodeToken(token: string){
    const decoded = JSON.parse(atob(token))
    console.log(decoded);
    return decoded
  }

  private addCharToToken(token: string){
    const alphabet = "abcdefghijklmnopqrstuvwxyz"

    const random = alphabet[Math.floor(Math.random() * alphabet.length)]
    return token.slice(0, 4) + random + token.slice(4);
  }

  private removeCharToToken(token: string) {
    console.log("removing")
    return token.slice(0, 4)  + token.slice(5);
  }

  getUserSite(): Site | null {
    const site = localStorage.getItem("site");
    if(site != null) return JSON.parse(site);
    return null
  }

  logoutUser() {
    localStorage.removeItem('user');
    localStorage.removeItem('auth');
    localStorage.removeItem('token');
    localStorage.removeItem('site');
    this.currentUserSource.next(null);
    this.site.set({} as Site)
    this.user.set(null)
    console.log("Logged out");
    window.location.reload();
  }
}
