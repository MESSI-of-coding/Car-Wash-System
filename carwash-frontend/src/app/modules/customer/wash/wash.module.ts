import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { WashNowComponent } from './components/wash-now/wash-now.component';
import { WashRoutingModule } from './wash-routing.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    WashRoutingModule,
    ReactiveFormsModule
  ]
})
export class WashModule { }
