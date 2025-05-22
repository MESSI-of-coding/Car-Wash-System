import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { PaymentRoutingModule } from './payment-routing.module';
import { PaymentComponent } from './components/payment.component';


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PaymentRoutingModule,
    PaymentComponent // Importing standalone component
  ]
})
export class PaymentModule {}
