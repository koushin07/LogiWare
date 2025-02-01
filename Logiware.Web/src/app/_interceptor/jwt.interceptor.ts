import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthenticationService } from '../_services/authentication.service';
import { take } from 'rxjs';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthenticationService)

  authService.currentUser$.pipe(take(1)).subscribe({

    next: (user) => {
      console.log(user?.token);

      if (user && user.token) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${user.token}`
          }
        });
      }
    }
  })
  return next(req);
};
