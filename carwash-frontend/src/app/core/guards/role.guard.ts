//Path: CarWash/carwash-frontend/src/app/core/guards/role.guard.ts
// Purpose: This guard checks if the user has the expected role by decoding the JWT token. If the role matches, the user is allowed to access the route.

import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Console } from 'console';

// Helper to decode JWT and extract role
function getRoleFromToken(token: string | null): string | null {
  if (!token) return null;
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    // Accept both 'role' and 'roles' (array or string)
    if (payload.role) return payload.role;
    console.log(payload);
    if (payload.roles && Array.isArray(payload.roles)) return payload.roles[0];
    return null;
  } catch {
    return null;
  }
}

export const RoleGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const expectedRoles = route.data['roles'] as string[];
  const token = auth.getToken();
  const userRole = getRoleFromToken(token);

  if (userRole && expectedRoles.includes(userRole)) {
    return true;
  } else {
    router.navigate(['/unauthorized']);
    return false;
  }
};