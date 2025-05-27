import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewService } from 'src/app/shared/services/review.service';
import { ToastService } from 'src/app/shared/services/toast.service'; // optional
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-submit-review',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './submit-review.component.html',
  styleUrls: ['./submit-review.component.scss']
})
export class SubmitReviewComponent implements OnInit {
  form!: FormGroup;
  requestId!: string;
  showSuccess = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private reviewService: ReviewService,
    private router: Router,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.requestId = this.route.snapshot.paramMap.get('requestId')!;
    this.form = this.fb.group({
      rating: [0, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    const { rating, comment } = this.form.value;

    this.reviewService.postReview({
      requestId: this.requestId,
      rating,
      comment
    }).subscribe({
      next: () => {
        this.showSuccess = true;
        this.toast.show('Thank you for your feedback!', 'success');
        setTimeout(() => {
          this.showSuccess = false;
          this.router.navigate(['/customer/wash/requests']);
        }, 2000);
      },
      error: err => {
        this.toast.show('Review submission failed', 'error');
        console.error(err);
      }
    });
  }
}
