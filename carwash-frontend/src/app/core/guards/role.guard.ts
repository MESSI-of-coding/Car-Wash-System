import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { AuthService } from '../services/auth.service';
import jwtDecode from 'jwt-decode';

export const roleGuard = (expectedRole: string): CanActivateFn => () => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const platformId = inject(PLATFORM_ID);

  if (isPlatformBrowser(platformId)) {
    const token = authService.getToken();

    if (token) {
      const decoded: any = jwtDecode(token);
      if (decoded?.role === expectedRole) {
        return true;
      }
    }

    router.navigate(['/unauthorized']);
  }

  return false;
};