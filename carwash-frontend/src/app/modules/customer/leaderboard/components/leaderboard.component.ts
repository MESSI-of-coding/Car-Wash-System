import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leaderboard.component.html',
  styleUrl: './leaderboard.component.scss'
})
export class LeaderboardComponent {
  leaderboard: Array<{
    rank: number;
    name: string;
    totalWaterSaved: number;
  }> = [];

  constructor() {
    // Example static data; replace with service call as needed
    this.leaderboard = [
      { rank: 1, name: 'Alice', totalWaterSaved: 1200 },
      { rank: 2, name: 'Bob', totalWaterSaved: 950 },
      { rank: 3, name: 'Charlie', totalWaterSaved: 800 }
    ];
  }
}
