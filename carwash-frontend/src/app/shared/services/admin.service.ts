import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AdminService {
  private baseUrl = `${environment.apiBaseUrl}/api/admin`;

  constructor(private http: HttpClient) {}

  // --- Users ---
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/users`);
  }

  toggleUserStatus(userId: string, isActive: boolean): Observable<any> {
    return this.http.put(`${this.baseUrl}/users/${userId}/status`, { isActive });
  }

  // --- Washers ---
  getWashers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/washers`);
  }

  exportWashersReport(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/washers/export`, {
      responseType: 'blob'
    });
  }

  // --- Cars ---
  getCars(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/cars`);
  }

  deleteCar(carId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/cars/${carId}`);
  }

  createOrUpdateCar(car: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/cars`, car);
  }

  // --- Packages ---
  getPackages(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/packages`);
  }

  createOrUpdatePackage(pkg: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/packages`, pkg);
  }

  deletePackage(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/packages/${id}`);
  }

  // --- AddOns ---
  getAddOns(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/addons`);
  }

  createOrUpdateAddOn(addon: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/addons`, addon);
  }

  deleteAddOn(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/addons/${id}`);
  }

  // --- Promos ---
  getPromos(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/promos`);
  }

  createOrUpdatePromo(promo: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/promos`, promo);
  }

  deletePromo(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/promos/${id}`);
  }

  // --- Reports ---
  exportReport(filters: { from: string; to: string; type: string }): Observable<Blob> {
    return this.http.post(`${this.baseUrl}/reports/export`, filters, { responseType: 'blob' });
  }

  getReportStats(filters: { from: string; to: string; type: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/reports/stats`, filters);
  }
}
