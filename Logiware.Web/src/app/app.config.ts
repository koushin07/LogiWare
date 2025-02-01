import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideAnimations} from "@angular/platform-browser/animations";
import {provideToastr} from "ngx-toastr";
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { DatePipe } from '@angular/common';

import { errorInterceptor } from './_interceptor/error.interceptor';
import { loadingInterceptor } from './_interceptor/loading.interceptor';
import { jwtInterceptor } from './_interceptor/jwt.interceptor';


export const appConfig: ApplicationConfig = {
  providers: [
    DatePipe,

    provideAnimations(), // required animations providers
    provideToastr(),
    provideHttpClient(withInterceptors([errorInterceptor, loadingInterceptor, jwtInterceptor  ])),
    provideCharts(withDefaultRegisterables()),
    provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes), provideCharts(withDefaultRegisterables())
  ]
};
