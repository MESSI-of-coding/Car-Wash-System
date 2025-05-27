import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { ReviewRoutingModule } from './review-routing.module';
import { SubmitReviewComponent } from './components/submit-review/submit-review.component';
import { WasherReviewsComponent } from './components/washer-reviews/washer-reviews.component';

@NgModule({
  // declarations: [
  //   SubmitReviewComponent,
  //   WasherReviewsComponent
  // ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ReviewRoutingModule
  ]
})
export class ReviewModule {}
