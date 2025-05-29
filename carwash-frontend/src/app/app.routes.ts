import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';
import { provideRouter } from '@angular/router';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  // { 
  //   path: '', 
  //   component: HomeComponent,// Eager-loaded
  //   //canActivate: [authGuard] 
  // },
  {
    path: '',
    loadComponent: () => import('./features/home/home.component').then(m => m.HomeComponent),
  },
  {
    path: 'login',
    loadComponent: () => import('./modules/auth/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'register',
    loadComponent: () => import('./modules/auth/register/register.component').then(m => m.RegisterComponent),
  },
  {
    path: 'customer',
    canActivate: [authGuard, RoleGuard],
    data: { roles: ['Customer'] },
    loadChildren: () => import('./modules/customer/customer.routes').then(m => m.routes),
  },
  {
    path: 'customer/profile',
    loadChildren: () => import('./modules/shared/profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'washer',
    canActivate: [authGuard, RoleGuard],
    data: { roles: ['Washer'] },
    loadChildren: () => import('./modules/washer/washer.module').then(m => m.WasherModule)
  },
  {
    path: 'washer/profile',
    loadChildren: () => import('./modules/shared/profile/profile.module').then(m => m.ProfileModule)
  },
  {
    path: 'admin',
    canActivate: [authGuard, RoleGuard],
    data: { roles: ['Admin'] },
    loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./components/unauthorized.component').then(m => m.UnauthorizedComponent)
  },
  {
    path: 'splash',
    loadComponent: () => import('./shared/splashscreen/splash.component').then(m => m.SplashComponent)
  },
  {
    path: '**',
    redirectTo: '',
  },
];

// export const appConfig = {
//   providers: [provideRouter(routes)],
// };
