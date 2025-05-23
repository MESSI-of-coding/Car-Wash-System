import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { provideRouter } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    loadComponent: () => import('./modules/auth/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () => import('./modules/auth/register.component').then(m => m.RegisterComponent),
  },
  {
    path: 'customer',
    canActivate: [authGuard, roleGuard('Customer')],
    loadChildren: () => import('./features/customer/customer.routes').then(m => m.routes),
  },
  {
    path: 'customer/cars',
    loadChildren: () =>
      import('./modules/customer/car/car.routes').then(m => m.routes)
  },
  {
    path: 'customer/wash',
    loadChildren: () =>
      import('./modules/customer/wash/wash.module').then(m => m.WashModule),
  },
  {
    path: 'customer/payment',
    loadChildren: () =>
      import('./modules/customer/payment/payment.module').then(m => m.PaymentModule)
  },
  {
    path: 'customer',
    children: [
      {
        path: 'review',
        loadChildren: () =>
          import('./modules/customer/review/review.module').then(m => m.ReviewModule)
      },
      {
        path: 'washers',
        loadChildren: () =>
          import('./modules/customer/review/review.module').then(m => m.ReviewModule)
      }
    ]
  },
  {
    path: 'customer/leaderboard',
    loadChildren: () =>
      import('./modules/customer/leaderboard/leaderboard.module').then(m => m.LeaderboardModule)
  },
  {
    path: 'washer',
    canActivate: [authGuard, roleGuard('Washer')],
    loadChildren: () => import('./features/washer/washer.routes').then(m => m.routes),
  },
  {
    path: 'admin',
    canActivate: [authGuard, roleGuard('Admin')],
    loadChildren: () => import('./features/admin/admin.routes').then(m => m.routes),
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./components/unauthorized.component').then(m => m.UnauthorizedComponent)
  },
  {
    path: 'customer/profile',
    loadChildren: () => import('./modules/shared/profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'washer/profile',
    loadChildren: () => import('./modules/shared/profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];

export const appConfig = {
  providers: [provideRouter(routes)],
};
