import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WashRequestDto, WashRequest } from 'src/app/core/models/wash-request.model';
import { WashPackage } from './wash-package.model';
import { WashRequest } from './wash-request.model';

@Injectable({
  providedIn: 'root'
})
export class WashService {
  private baseUrl = `${environment.apiBaseUrl}/api/wash-requests`;

  constructor(private http: HttpClient) { }

  getPackages(): Observable<WashPackage[]> {
    return this.http.get<WashPackage[]>(`${this.baseUrl}/api/packages`);
  }

  createRequest(dto: WashRequestDto): Observable<WashRequest> {
    return this.http.post<WashRequest>(`${this.baseUrl}`, dto);
  }

  getRequestsByUser(): Observable<WashRequest[]> {
    return this.http.get<WashRequest[]>(`${this.baseUrl}/user`);
  }
}