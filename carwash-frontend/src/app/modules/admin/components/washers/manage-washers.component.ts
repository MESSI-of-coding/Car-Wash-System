import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-manage-washers',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-washers.component.html',
  styleUrls: ['./manage-washers.component.scss']
})
export class ManageWashersComponent implements OnInit {
  washers: any[] = [];
  searchQuery: string = '';
  headers = [
    { key: 'userId', label: 'ID' },
    { key: 'fullName', label: 'Name' },
    { key: 'email', label: 'Email' },
    { key: 'isActive', label: 'Status' },
    { key: 'actions', label: 'Actions' }
  ];
  sortColumn: string = 'userId';
  sortDirection: 'asc' | 'desc' = 'asc';
  loading = false;
  currentPage = 1;
  itemsPerPage = 10;

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadWashers();
  }

  loadWashers(): void {
    this.loading = true;
    this.adminService.getWashers().subscribe(data => {
      this.washers = data;
      this.loading = false;
    }, () => this.loading = false);
  }

  get filteredWashers() {
    let filtered = this.washers.filter(w =>
      !this.searchQuery ||
      w.fullName?.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      w.email?.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
    filtered = filtered.sort((a, b) => {
      const aValue = a[this.sortColumn];
      const bValue = b[this.sortColumn];
      if (aValue == null) return 1;
      if (bValue == null) return -1;
      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
    const start = (this.currentPage - 1) * this.itemsPerPage;
    return filtered.slice(start, start + this.itemsPerPage);
  }

  sortTable(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }
  }

  get currentPageStart() {
    return (this.currentPage - 1) * this.itemsPerPage + 1;
  }
  get currentPageEnd() {
    return Math.min(this.currentPage * this.itemsPerPage, this.washers.length);
  }

  previousPage() {
    if (this.currentPage > 1) this.currentPage--;
  }
  nextPage() {
    if (this.currentPage * this.itemsPerPage < this.washers.length) this.currentPage++;
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
