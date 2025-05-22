//Path: CarWash/carwash-frontend/src/app/core/guards/auth.guard.ts
// Purpose: This guard checks if the user is authenticated by checking if a token exists in local storage. If the token exists, the user is allowed to access the route. 
// If not, the user is redirected to the login page.

import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router'; 
import { isPlatformBrowser } from '@angular/common';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

  if (isPlatformBrowser(platformId)) {
    const token = authService.getToken();
    if (token) {
      return true;
    }
    router.navigate(['/login']);
  }

  return false;
};