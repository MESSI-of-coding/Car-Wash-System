import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';

@NgModule({
  
  imports: [
    CommonModule,
    AdminRoutingModule,
    NgxChartsModule,
    DashboardComponent
  ]
})
export class AdminModule { }
