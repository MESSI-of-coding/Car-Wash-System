//Path: CarWash/carwash-frontend/src/app/core/services/auth.service.ts
// Purpose: This service handles authentication-related operations such as login, logout, and registration.
// It uses HttpClient to make HTTP requests to the backend API.


import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginResponse } from '../models/login-response.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private platformId = inject(PLATFORM_ID);
  private apiUrl = 'http://localhost:5162/api/auth';


  constructor(private http: HttpClient) { }

  // login(email: string, password: string): Observable<any> {
  //   return this.http.post(`${this.apiUrl}/login`, { email, password });
  // }

  login( email: string, password: string ): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/auth/login`, { email, password });
  }

  logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
    }
  }

  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('token');
    }
    return null; // SSR: localStorage not available
  }

  register(data: { fullName: string; email: string; password: string; contactNumber?: string; role: string; location: { latitude: number; longitude: number } }): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }
}