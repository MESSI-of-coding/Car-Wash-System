import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-washers',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-washers.component.html',
  styleUrls: ['./manage-washers.component.scss']
})
export class ManageWashersComponent implements OnInit {
  washers: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadWashers();
  }

  loadWashers(): void {
    this.adminService.getWashers().subscribe(data => {
      this.washers = data;
    });
  }

  toggleStatus(washer: any): void {
    const newStatus = !washer.isActive;
    this.adminService.toggleUserStatus(washer.userId, newStatus).subscribe(() => {
      washer.isActive = newStatus;
    });
  }

  exportCSV(): void {
    this.adminService.exportWashersReport().subscribe(blob => {
      const link = document.createElement('a');
      const url = window.URL.createObjectURL(blob);
      link.href = url;
      link.download = 'washers_report.csv';
      link.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
