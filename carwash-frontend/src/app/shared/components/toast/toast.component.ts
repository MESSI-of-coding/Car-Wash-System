import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from 'src/app/shared/services/toast.service';
import { COMMON_IMPORTS } from 'src/app/shared/common-imports';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule, COMMON_IMPORTS],
  template: `
    <div *ngIf="toastService.toast$ | async as toast"
         [ngClass]="{
           'bg-green-500': toast.type === 'success',
           'bg-red-500': toast.type === 'error',
           'bg-blue-500': toast.type === 'info'
         }"
         class="fixed bottom-4 right-4 text-white px-4 py-2 rounded shadow-lg z-50">
      {{ toast.message }}
    </div>
  `,
})
export class ToastComponent {
  toastService = inject(ToastService);
}
