import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from '../../environments/environment.development';
import { delay, finalize, identity } from 'rxjs';
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const spinnerService = inject(NgxSpinnerService);

  // List of base paths where you want to disable the spinner (ignoring dynamic segments)
  const excludedBasePaths = ['/Personnel/authorize'];

  // Optionally, check for a custom header to disable the spinner

  // Function to check if the request URL matches any of the excluded base paths
  const shouldExclude = excludedBasePaths.some(basePath => req.url.startsWith(basePath));

  // Condition to skip showing the spinner
  const shouldShowSpinner = !shouldExclude;

  if (shouldShowSpinner) {
    spinnerService.show();
  }

  return next(req).pipe(
    (environment.production ? identity : delay(1000)),
    finalize(() => {
      if (shouldShowSpinner) {
        spinnerService.hide();
      }
    })
  );
};

