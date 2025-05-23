import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SubmitReviewComponent } from './components/submit-review.component';
import { WasherReviewsComponent } from './components/washer-reviews.component';

const routes: Routes = [
  { path: 'review/:requestId', component: SubmitReviewComponent },
  { path: 'washers/:washerId/reviews', component: WasherReviewsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReviewRoutingModule {}
