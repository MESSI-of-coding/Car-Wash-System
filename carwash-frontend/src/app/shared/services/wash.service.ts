import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WashRequestDto, WashRequest } from 'src/app/core/models/wash-request.model';

 @Injectable({
  providedIn: 'root'
})
export class WashService {
  private baseUrl = `${environment.apiBaseUrl}/api/wash-requests`;

  constructor(private http: HttpClient) { }

  getPackages(): Observable<any[]> { // Temporarily use any[] until WashPackage model is created
    return this.http.get<any[]>(`${this.baseUrl}/api/packages`);
  }

  createRequest(dto: WashRequestDto): Observable<any> { // Temporarily use any until WashRequest model is confirmed
    return this.http.post<any>(`${this.baseUrl}`, dto);
  }

  getRequestsByUser(): Observable<any[]> { // Temporarily use any[] until WashRequest model is confirmed
    return this.http.get<any[]>(`${this.baseUrl}/user`);
  }
}