import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';

export const adminGuard: CanActivateFn = (route, state) => {
  const authSerivce = inject(AuthenticationService)
   const router = inject(Router);

  return  authSerivce.currentUser$.pipe(
    map((user) => {
       if (user?.user.username == "admin") {
         return true;
       } else {
         router.navigateByUrl('/')

         return false;
       }
     })
   )

};
