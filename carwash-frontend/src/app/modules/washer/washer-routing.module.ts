import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RoleGuard } from 'src/app/core/guards/role.guard';
import { WasherDashboardComponent } from './components/dashboard/dashboard.component';
import { MyOrdersComponent } from './components/my-orders/my-orders.component';
import { WashRequestDetailComponent } from './components/wash-request-detail/wash-request-detail.component';
// import { WasherProfileComponent } from './components/profile/profile.component';
import { ProfileComponent } from '../shared/profile/components/profile.component';
import { WasherNotificationComponent } from './components/notifications/notifications.component';
import { WasherReviewComponent } from './components/reviews/reviews.component';

const routes: Routes = [
  {
    path: '',
    component: WasherDashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  },
  {
    path: 'orders',
    component: MyOrdersComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  },
  {
    path: 'request/:id',
    component: WashRequestDetailComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  },
  {
    path: 'reviews',
    component: WasherReviewComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  },
  {
    path: 'notifications',
    component: WasherNotificationComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Washer'] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WasherRoutingModule {}
