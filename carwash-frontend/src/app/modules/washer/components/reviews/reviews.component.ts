import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WasherService } from '../../services/washer.service';

@Component({
  selector: 'app-washer-review',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class WasherReviewComponent implements OnInit {
  submittedReviews: any[] = [];
  reviewForm = {
    requestId: 0,
    rating: 0,
    comment: ''
  };
  isSubmitting = false;

  // UI error flags
  showRequestIdError = false;
  showRatingError = false;
  showCommentError = false;

  constructor(private washerService: WasherService) {}

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews(): void {
    this.washerService.getWasherSubmittedReviews().subscribe(data => {
      this.submittedReviews = data;
    });
  }

  validateForm(): boolean {
    this.showRequestIdError = !this.reviewForm.requestId || this.reviewForm.requestId <= 0;
    this.showRatingError = this.reviewForm.rating < 1 || this.reviewForm.rating > 5;
    this.showCommentError = !this.reviewForm.comment || this.reviewForm.comment.trim().length < 5;
    return !(this.showRequestIdError || this.showRatingError || this.showCommentError);
  }

  submitReview(): void {
    if (!this.validateForm()) {
      return;
    }
    this.isSubmitting = true;
    this.washerService.submitCustomerReview(this.reviewForm).subscribe({
      next: () => {
        alert('Review submitted!');
        this.reviewForm = { requestId: 0, rating: 0, comment: '' };
        this.isSubmitting = false;
        this.loadReviews();
      },
      error: () => {
        alert('Failed to submit review.');
        this.isSubmitting = false;
      }
    });
  }
}