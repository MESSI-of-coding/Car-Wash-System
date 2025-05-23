import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { ManageUsersComponent } from './components/manage-users.component';
import { ManageWashersComponent } from './components/manage-washers.component';
import { ManageCarsComponent } from './components/manage-cars.component';
import { ManagePackagesComponent } from './components/manage-packages.component';
import { ManageAddOnsComponent } from './components/manage-addons.component';
import { ManagePromosComponent } from './components/manage-promos.component';
import { ReportsDashboardComponent } from './components/reports-dashboard.component';
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
  { path: 'addons', component: ManageAddOnsComponent, canActivate: [RoleGuard], data: { roles: ['Admin'] } },
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
