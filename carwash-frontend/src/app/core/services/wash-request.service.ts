import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WashRequestDto, WashRequest } from '../models/wash-request.model';

@Injectable({
  providedIn: 'root'
})
export class WashRequestService {
  private baseUrl = '/api/washrequests';

  constructor(private http: HttpClient) {}

  createWashRequest(request: WashRequestDto): Observable<WashRequest> {
    return this.http.post<WashRequest>(`${this.baseUrl}`, request);
  }

  getCustomerRequests(customerId: string): Observable<WashRequest[]> {
    return this.http.get<WashRequest[]>(`${this.baseUrl}/customer/${customerId}`);
  }

  getWasherRequests(washerId: string): Observable<WashRequest[]> {
    return this.http.get<WashRequest[]>(`${this.baseUrl}/washer/${washerId}`);
  }

  getRequestById(requestId: string): Observable<WashRequest> {
    return this.http.get<WashRequest>(`${this.baseUrl}/${requestId}`);
  }

  updateRequestStatus(requestId: string, status: string): Observable<WashRequest> {
    return this.http.patch<WashRequest>(`${this.baseUrl}/${requestId}/status`, { status });
  }
}
