import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'app-primary-button',
  template: `<button class="btn-primary"><ng-content></ng-content></button>`,
  styles: [`
    .btn-primary { padding: 8px 16px; background: blue; color: white; }
  `]
})
export class PrimaryButtonComponent {}