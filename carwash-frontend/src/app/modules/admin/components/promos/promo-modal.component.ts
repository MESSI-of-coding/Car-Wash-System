import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromoDto } from './manage-promos.component';

@Component({
  selector: 'app-promo-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './promo-modal.component.html',
  styleUrl: './promo-modal.component.scss'
})
export class PromoModalComponent {
  @Input() promo: PromoDto | null = null;
  @Output() close = new EventEmitter<boolean>();

  closeModal() {
    this.close.emit(false);
  }

  savePromo() {
    // emit true to reload if saved
    this.close.emit(true);
  }
}
