import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { LeaderboardEntry } from 'src/app/core/models/leaderboard-entry.model';

@Injectable({ providedIn: 'root' })
export class LeaderboardService {
  private baseUrl = `${environment.apiBaseUrl}/api/leaderboard`;

  constructor(private http: HttpClient) {}

  getTopWashers(): Observable<LeaderboardEntry[]> {
    return this.http.get<LeaderboardEntry[]>(this.baseUrl);
  }
}
