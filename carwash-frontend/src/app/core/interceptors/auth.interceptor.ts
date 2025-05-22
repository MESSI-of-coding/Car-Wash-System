//Path: CarWash/carwash-frontend/src/app/core/interceptors/auth.interceptor.ts
// Purpose: This interceptor adds the Authorization header with the JWT token to outgoing HTTP requests if the token is available. 
// It uses the AuthService to retrieve the token.

import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.authService.getToken(); // Assumes getToken() returns the JWT
    let authReq = req;
    if (token) {
      authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    return next.handle(authReq);
  }
}
