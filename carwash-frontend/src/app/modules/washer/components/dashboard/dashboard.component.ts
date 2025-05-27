import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WasherService } from '../../services/washer.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-washer-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class WasherDashboardComponent implements OnInit {
  stats: any = null;
  washerName: string = '';
  currentDate: Date = new Date();
  mapsUrl: string = '';
  jobStatusClass: string = '';

  constructor(private washerService: WasherService) {}

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.washerService.getWasherDashboardStats().subscribe(data => {
      this.stats = data;
      this.setWasherName();
      this.setMapsUrl();
      this.setJobStatusClass();
      this.currentDate = new Date();
    });
  }

  setWasherName(): void {
    // Try to get washer name from stats or fallback to localStorage/user profile
    this.washerName = this.stats?.washerName || 'Washer';
  }

  setMapsUrl(): void {
    if (this.stats?.nextJob?.location) {
      const loc = this.stats.nextJob.location;
      this.mapsUrl = `https://www.google.com/maps/search/?api=1&query=${loc.latitude},${loc.longitude}`;
    } else {
      this.mapsUrl = '';
    }
  }

  setJobStatusClass(): void {
    const status = this.stats?.nextJob?.status || '';
    switch (status.toLowerCase()) {
      case 'pending':
        this.jobStatusClass = 'bg-yellow-100 text-yellow-800';
        break;
      case 'accepted':
        this.jobStatusClass = 'bg-blue-100 text-blue-800';
        break;
      case 'inprogress':
      case 'in progress':
        this.jobStatusClass = 'bg-green-100 text-green-800';
        break;
      case 'completed':
        this.jobStatusClass = 'bg-gray-100 text-gray-800';
        break;
      default:
        this.jobStatusClass = 'bg-gray-100 text-gray-800';
    }
  }

  refreshDashboard(): void {
    this.loadDashboard();
  }

  acceptJob(): void {
    // TODO: Implement accept job logic (call washerService.acceptJob etc.)
    alert('Accept job feature coming soon!');
  }

  startJob(): void {
    // TODO: Implement start job logic (call washerService.startJob etc.)
    alert('Start job feature coming soon!');
  }

  requestReschedule(): void {
    // TODO: Implement reschedule logic (call washerService.requestReschedule etc.)
    alert('Reschedule feature coming soon!');
  }

  checkAvailableJobs(): void {
    // TODO: Implement check available jobs logic (navigate or call washerService)
    alert('Check available jobs feature coming soon!');
  }
}
