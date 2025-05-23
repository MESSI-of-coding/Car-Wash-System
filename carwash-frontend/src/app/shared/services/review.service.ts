import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ReviewDto, Review } from 'src/app/core/models/review.model';

@Injectable({ providedIn: 'root' })
export class ReviewService {
  private baseUrl = `${environment.apiBaseUrl}/api/reviews`;

  constructor(private http: HttpClient) {}

  postReview(dto: ReviewDto): Observable<any> {
    return this.http.post(this.baseUrl, dto);
  }

  getReviews(washerId: string): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.baseUrl}/washer/${washerId}`);
  }
}
