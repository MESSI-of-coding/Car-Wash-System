//Path: CarWash/carwash-frontend/src/app/core/core.module.ts
// Purpose: This module is responsible for providing core services and interceptors for the application. 
// It ensures that the CoreModule is only imported once in the AppModule to prevent multiple instances of services.

import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule should only be imported in AppModule.');
    }
  }
}
