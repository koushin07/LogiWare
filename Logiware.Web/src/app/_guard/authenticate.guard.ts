import { CanActivateFn, Router } from '@angular/router';
import {inject} from "@angular/core";
import {AuthenticationService} from "../_services/authentication.service";
import {map} from "rxjs";
import { toast } from 'ngx-sonner';

export const authenticateGuard: CanActivateFn = (route, state) => {
  const authSerivce = inject(AuthenticationService)
  const router = inject(Router);

 return  authSerivce.currentUser$.pipe(
    map((user)=>{
      if (user) {
        return true;
      } else {
        router.navigateByUrl('/')

        return false;
      }
    })
  )

};
