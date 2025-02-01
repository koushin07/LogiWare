import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { toast } from 'ngx-sonner';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error) {
        console.log(error)
      Object.keys(error.error.errors).forEach((key) => {
  // Combine the error messages for the current key into a single string
  const message = error.error.errors[key].join(' ');

  // Display the toast with the key as the title and the combined message as the description
  toast.error(key, {
    description: message
  });
});
        // switch (error.status) {
        //   case 401:
        //     toast.error('Unauthorized', {
        //       description: error.error.message,

        //    })
        //     break;
        //   case 403:
        //    toast.error('Forbidden', {
        //       description: error.message,

        //    })
        //     break;
        //   case 404:
        //       toast.error('Not Found', {
        //       description: error.message,

        //    })
        //     break;
        //   case 500:
        //        toast.error('Internal Server', {
        //       description: error.message,

        //    })
        //     break;
        //   default:
        //       toast.error('Unknown Error', {
        //       description: error.message,

        //    })
        //     break;
        // }
      }
      throw error;
    })
  );
};
