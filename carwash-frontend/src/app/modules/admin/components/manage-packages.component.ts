import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import { finalize } from 'rxjs/operators';

export interface PackageDto {
  packageId: number;
  name: string;
  description: string;
  price: number;
  duration: number;
  // Add other fields as needed
}

@Component({
  selector: 'app-manage-packages',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './manage-packages.component.html'
})
export class ManagePackagesComponent implements OnInit {
  packages: PackageDto[] = [];
  filteredPackages: PackageDto[] = [];
  loading = false;
  errorMsg = '';
  searchQuery = '';
  sortColumn: keyof PackageDto | '' = '';
  sortDirection: 'asc' | 'desc' = 'asc';
  currentPage = 1;
  itemsPerPage = 10;
  showModal = false;
  selectedPackage: PackageDto | null = null;
  form: FormGroup;
  headers = [
    { key: 'packageId', label: 'ID' },
    { key: 'name', label: 'Name' },
    { key: 'description', label: 'Description' },
    { key: 'price', label: 'Price' },
    { key: 'duration', label: 'Duration' }
  ];

  constructor(private adminService: AdminService, private fb: FormBuilder) {
    this.form = this.fb.group({
      packageId: [null],
      name: ['', Validators.required],
      description: [''],
      price: ['', [Validators.required, Validators.min(0)]],
      duration: ['', [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.errorMsg = '';
    this.adminService.getPackages()
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: res => {
          this.packages = res;
          this.applyFilters();
        },
        error: err => {
          this.errorMsg = 'Failed to load packages.';
        }
      });
  }

  save() {
    if (this.form.invalid) return;
    this.loading = true;
    const dto = this.form.value as PackageDto;
    this.adminService.createOrUpdatePackage(dto)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => {
          this.closeModal(true);
        },
        error: () => {
          this.errorMsg = 'Failed to save package.';
        }
      });
  }

  applyFilters() {
    let data = [...this.packages];
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      data = data.filter(pkg =>
        pkg.name.toLowerCase().includes(q) ||
        (pkg.description && pkg.description.toLowerCase().includes(q))
      );
    }
    if (this.sortColumn) {
      data = data.sort((a, b) => {
        const aVal = a[this.sortColumn as keyof PackageDto];
        const bVal = b[this.sortColumn as keyof PackageDto];
        if (aVal == null || bVal == null) return 0;
        if (aVal < bVal) return this.sortDirection === 'asc' ? -1 : 1;
        if (aVal > bVal) return this.sortDirection === 'asc' ? 1 : -1;
        return 0;
      });
    }
    // Pagination
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.filteredPackages = data.slice(start, end);
  }

  get currentPageStart() {
    return (this.currentPage - 1) * this.itemsPerPage + 1;
  }
  get currentPageEnd() {
    return Math.min(this.currentPage * this.itemsPerPage, this.filteredPackages.length);
  }

  sortTable(column: keyof PackageDto) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }
    this.applyFilters();
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.applyFilters();
    }
  }
  nextPage() {
    if (this.currentPage * this.itemsPerPage < this.packages.length) {
      this.currentPage++;
      this.applyFilters();
    }
  }

  openAddEditModal(pkg?: PackageDto) {
    this.selectedPackage = pkg ? { ...pkg } : null;
    this.form.reset();
    if (pkg) {
      this.form.patchValue(pkg);
    }
    this.showModal = true;
  }

  closeModal(reload: boolean = false) {
    this.showModal = false;
    this.selectedPackage = null;
    this.form.reset();
    if (reload) this.load();
  }

  deletePackage(pkg: PackageDto) {
    if (!confirm(`Delete package "${pkg.name}"?`)) return;
    this.loading = true;
    this.adminService.deletePackage(pkg.packageId)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => this.load(),
        error: () => { this.errorMsg = 'Failed to delete package.'; }
      });
  }

  exportCSV() {
    // Simple CSV export for visible packages
    const headers = this.headers.map(h => h.label).join(',');
    const rows = this.filteredPackages.map(pkg =>
      [pkg.packageId, pkg.name, pkg.description, pkg.price, pkg.duration].join(',')
    );
    const csv = [headers, ...rows].join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'packages.csv';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  // Watch for changes in searchQuery and pagination
  ngDoCheck() {
    this.applyFilters();
  }
}
