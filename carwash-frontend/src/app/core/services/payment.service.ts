import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private apiUrl = `${environment.apiBaseUrl}/api/payments`;

  constructor(private http: HttpClient) {}

  getClientSecret(requestId: string): Observable<{ clientSecret: string }> {
    return this.http.get<{ clientSecret: string }>(`${this.apiUrl}/client-secret/${requestId}`);
  }
}
