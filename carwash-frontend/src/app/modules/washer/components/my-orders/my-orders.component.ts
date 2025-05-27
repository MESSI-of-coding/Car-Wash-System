import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { WasherService } from '../../services/washer.service';

@Component({
  selector: 'app-my-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent implements OnInit {
  currentOrders: any[] = [];
  pastOrders: any[] = [];
  activeTab: 'current' | 'past' = 'current';
  isLoading = false;

  constructor(private washerService: WasherService, private router: Router) {}

  ngOnInit(): void {
    this.fetchOrders();
  }

  fetchOrders(): void {
    this.isLoading = true;
    this.washerService.getMyOrders().subscribe((orders: any[]) => {
      this.currentOrders = orders.filter(o => ['Accepted', 'InProgress'].includes(o.status));
      this.pastOrders = orders.filter(o => ['Completed', 'Cancelled'].includes(o.status));
      this.isLoading = false;
    }, () => this.isLoading = false);
  }

  setTab(tab: 'current' | 'past') {
    this.activeTab = tab;
  }

  completeOrder(orderId: number) {
    // TODO: Implement complete & generate invoice logic
    alert('Complete & Generate Invoice feature coming soon!');
  }

  getStatusClass(status: string): string {
    switch ((status || '').toLowerCase()) {
      case 'pending': return 'bg-yellow-100 text-yellow-800';
      case 'accepted': return 'bg-blue-100 text-blue-800';
      case 'inprogress':
      case 'in progress': return 'bg-indigo-100 text-indigo-800';
      case 'completed': return 'bg-green-100 text-green-800';
      case 'cancelled': return 'bg-red-100 text-red-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }

  getMapLink(order: any): string {
    return `https://www.google.com/maps/search/?api=1&query=${order.latitude},${order.longitude}`;
  }

  goToInvoice(requestId: number) {
    // TODO: Implement navigation to invoice page
    alert('Complete & Generate Invoice feature coming soon!');
  }

  viewReceipt(requestId: number) {
    // TODO: Implement view receipt logic
    alert('View Receipt feature coming soon!');
  }
}
