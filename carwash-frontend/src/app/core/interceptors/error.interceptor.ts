//Path: CarWash/carwash-frontend/src/app/core/interceptors/error.interceptor.ts
// Purpose: This interceptor handles HTTP errors globally. It catches errors from HTTP requests and logs them to the console.


import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptorFn,
  HttpRequest,
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError(err => {
      // You could integrate ToastService here
      console.error('API error', err);
      return throwError(() => err);
    })
  );
};

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: errorInterceptor,
  multi: true
};
