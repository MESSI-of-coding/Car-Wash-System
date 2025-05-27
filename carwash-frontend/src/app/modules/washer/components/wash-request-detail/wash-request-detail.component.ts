import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { WasherService } from '../../services/washer.service';

@Component({
  selector: 'app-wash-request-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wash-request-detail.component.html',
  styleUrls: ['./wash-request-detail.component.scss']
})
export class WashRequestDetailComponent implements OnInit {
  requestId!: number;
  request: any;
  isProcessing = false;

  constructor(
    private route: ActivatedRoute,
    private washerService: WasherService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.requestId = +this.route.snapshot.paramMap.get('id')!;
    this.washerService.getWashRequest(this.requestId).subscribe(data => {
      this.request = data;
    });
  }
  accept() {
    this.isProcessing = true;
    this.washerService.acceptRequest(this.requestId).subscribe({
      next: () => {
        alert('Request accepted.');
        this.router.navigate(['/washer/orders']);
      },
      complete: () => this.isProcessing = false,
      error: () => this.isProcessing = false
    });
  }

  decline() {
    this.isProcessing = true;
    this.washerService.declineRequest(this.requestId).subscribe({
      next: () => {
        alert('Request declined.');
        this.router.navigate(['/washer/orders']);
      },
      complete: () => this.isProcessing = false,
      error: () => this.isProcessing = false
    });
  }

  get statusBadgeClass(): string {
    switch ((this.request?.status || '').toLowerCase()) {
      case 'pending': return 'bg-yellow-100 text-yellow-800';
      case 'accepted': return 'bg-blue-100 text-blue-800';
      case 'inprogress':
      case 'in progress': return 'bg-indigo-100 text-indigo-800';
      case 'completed': return 'bg-green-100 text-green-800';
      case 'cancelled': return 'bg-red-100 text-red-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  getMapLink(): string {
    return `https://www.google.com/maps/search/?api=1&query=${this.request?.latitude},${this.request?.longitude}`;
  }
}
