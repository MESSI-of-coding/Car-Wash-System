import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private baseUrl = `${environment.apiBaseUrl}/api/payments`;

  constructor(private http: HttpClient) {}

  getClientSecret(requestId: string): Observable<{ clientSecret: string }> {
    return this.http.get<{ clientSecret: string }>(`${this.baseUrl}/secret/${requestId}`);
  }
}
