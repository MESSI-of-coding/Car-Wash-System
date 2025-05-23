import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://your-api-url.com';

  constructor(private http: HttpClient) { }

  getProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/api/users/me`);
  }

  updateProfile(dto: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/api/users/me`, dto);
  }

  getWasherStats(userId: number): Observable<{ averageRating: number, totalReviews: number }> {
    return this.http.get<{ averageRating: number, totalReviews: number }>(
      `${this.baseUrl}/api/reviews/stats/${userId}`
    );
  }
}