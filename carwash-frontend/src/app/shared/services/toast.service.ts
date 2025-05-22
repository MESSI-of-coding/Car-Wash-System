import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface ToastMessage {
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private toastSubject = new BehaviorSubject<ToastMessage | null>(null);
  toast$ = this.toastSubject.asObservable();

  show(message: string, type: ToastMessage['type'] = 'info') {
    this.toastSubject.next({ message, type });

    // Auto-hide after 3 seconds
    setTimeout(() => this.toastSubject.next(null), 3000);
  }
}