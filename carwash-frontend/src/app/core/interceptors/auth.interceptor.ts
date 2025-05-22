//Path: CarWash/carwash-frontend/src/app/core/interceptors/auth.interceptor.ts
// Purpose: This interceptor adds the Authorization header with the JWT token to outgoing HTTP requests if the token is available. 
// It uses the AuthService to retrieve the token.


import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  if (token) {
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(authReq);
  }

  return next(req);
};
