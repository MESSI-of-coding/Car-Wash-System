import { provideRouter, Routes } from '@angular/router';
import { authRoutes } from './modules/auth/auth.routes';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [

  ...authRoutes, // Spreads the auth-related routes
  {
    path: 'customer',
    loadChildren: () => import('./features/customer/customer.routes').then(m => m.routes),
    canActivate: [authGuard, roleGuard(['Customer'])]
  },
  {
    path: 'customer/cars',
    loadChildren: () =>
      import('./modules/customer/car/car.routes').then(m => m.routes)
  },
  {
    path: 'washer',
    loadChildren: () => import('./features/washer/washer.routes').then(m => m.routes),
    canActivate: [authGuard, roleGuard(['Washer'])]
  },
  {
    path: 'admin',
    loadChildren: () => import('./features/admin/admin.routes').then(m => m.routes),
    canActivate: [authGuard, roleGuard(['Admin'])]
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./components/unauthorized.component').then(m => m.UnauthorizedComponent)
  }
];

export const appConfig = {
  providers: [provideRouter(routes)],
};
