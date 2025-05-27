import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class WasherService {
  private baseUrl = environment.apiBaseUrl + '/api';

  constructor(private http: HttpClient) {}

  getWasherDashboardStats(): Observable<any> {
    return this.http.get(`${this.baseUrl}/washer/dashboard`);
  }

  getWashRequest(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/washer/wash-requests/${id}`);
  }

  acceptRequest(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/washer/wash-requests/${id}/accept`, {});
  }

  declineRequest(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/washer/wash-requests/${id}/decline`, {});
  }

  getMyOrders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/washer/my-orders`);
  }

  getWasherSubmittedReviews(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/washer/reviews/given`);
  }

  submitCustomerReview(reviewDto: { requestId: number; rating: number; comment: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/washer/reviews`, reviewDto);
  }

  getProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/washer/profile`);
  }

  updateProfile(formData: FormData): Observable<any> {
    return this.http.put(`${this.baseUrl}/washer/profile`, formData);
  }

  getNotifications(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/washer/notifications`);
  }
}
