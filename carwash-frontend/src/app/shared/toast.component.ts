import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from './toast.service';
import { COMMON_IMPORTS } from './common-imports';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule, COMMON_IMPORTS],
  templateUrl: './toast.component.html',
})
export class ToastComponent {
  toastService = inject(ToastService);
}
