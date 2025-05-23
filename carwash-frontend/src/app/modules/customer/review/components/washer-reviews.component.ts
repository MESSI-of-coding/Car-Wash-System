import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReviewService } from 'src/app/shared/services/review.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-washer-reviews',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './washer-reviews.component.html',
  styleUrls: ['./washer-reviews.component.scss']
})
export class WasherReviewsComponent implements OnInit {
  washerId!: string;
  reviews: { rating: number; comment: string; date: string }[] = [];
  averageRating: number = 0;

  constructor(
    private route: ActivatedRoute,
    private reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    this.washerId = this.route.snapshot.paramMap.get('washerId')!;
    this.reviewService.getReviewsByWasher(this.washerId).subscribe(data => {
      this.reviews = data;
      if (data.length) {
        this.averageRating = data.reduce((sum, r) => sum + r.rating, 0) / data.length;
      }
    });
  }
}
