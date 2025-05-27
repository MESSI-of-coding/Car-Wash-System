import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { ManageUsersComponent } from './components/users/manage-users.component';
import { ManageWashersComponent } from './components/washers/manage-washers.component';
import { ManageCarsComponent } from './components/cars/manage-cars.component';
import { ManagePackagesComponent } from './components/packages/manage-packages.component';
import { ManageAddonsComponent } from './components/addons/manage-addons.component';
import { ManagePromosComponent } from './components/promos/manage-promos.component';
import { ReportsDashboardComponent } from './components/reports/reports-dashboard.component';
import { RoleGuard } from 'src/app/core/guards/role.guard';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'users',
    component: ManageUsersComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin'] }
  },
  {
    path: 'washers',
    component: ManageWashersComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin'] }
  },
  { path: 'cars', component: ManageCarsComponent, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'packages', component: ManagePackagesComponent, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'addons', component: ManageAddonsComponent, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  { path: 'promos', component: ManagePromosComponent, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
  {
    path: 'reports',
    component: ReportsDashboardComponent,
    canActivate: [RoleGuard],
    data: { roles: ['Admin'] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {}
