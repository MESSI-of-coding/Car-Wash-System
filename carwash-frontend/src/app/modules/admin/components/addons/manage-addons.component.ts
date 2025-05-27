import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import { finalize } from 'rxjs/operators';
import { AddonModalComponent } from './addon-modal.component';

export interface AddOnDto {
  addOnId: number;
  name: string;
  description: string;
  price: number;
}

@Component({
  selector: 'app-manage-addons',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, AddonModalComponent],
  templateUrl: './manage-addons.component.html'
})
export class ManageAddonsComponent implements OnInit {
  addOns: AddOnDto[] = [];
  filteredAddons: AddOnDto[] = [];
  loading = false;
  errorMsg = '';
  searchQuery = '';
  sortColumn: keyof AddOnDto | '' = '';
  sortDirection: 'asc' | 'desc' = 'asc';
  currentPage = 1;
  itemsPerPage = 10;
  showModal = false;
  selectedAddOn: AddOnDto | null = null;
  form: FormGroup;
  headers = [
    { key: 'addOnId' as keyof AddOnDto, label: 'ID' },
    { key: 'name' as keyof AddOnDto, label: 'Name' },
    { key: 'description' as keyof AddOnDto, label: 'Description' },
    { key: 'price' as keyof AddOnDto, label: 'Price' }
  ];

  constructor(private adminService: AdminService, private fb: FormBuilder) {
    this.form = this.fb.group({
      addOnId: [null],
      name: ['', Validators.required],
      description: [''],
      price: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.errorMsg = '';
    this.adminService.getAddOns()
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: res => {
          this.addOns = res;
          this.applyFilters();
        },
        error: err => {
          this.errorMsg = 'Failed to load add-ons.';
        }
      });
  }

  applyFilters() {
    let data = [...this.addOns];
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      data = data.filter(addon =>
        addon.name.toLowerCase().includes(q) ||
        (addon.description && addon.description.toLowerCase().includes(q))
      );
    }
    if (this.sortColumn) {
      data = data.sort((a, b) => {
        const aVal = a[this.sortColumn as keyof AddOnDto];
        const bVal = b[this.sortColumn as keyof AddOnDto];
        if (aVal == null || bVal == null) return 0;
        if (aVal < bVal) return this.sortDirection === 'asc' ? -1 : 1;
        if (aVal > bVal) return this.sortDirection === 'asc' ? 1 : -1;
        return 0;
      });
    }
    // Pagination
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.filteredAddons = data.slice(start, end);
  }

  get currentPageStart() {
    return (this.currentPage - 1) * this.itemsPerPage + 1;
  }
  get currentPageEnd() {
    return Math.min(this.currentPage * this.itemsPerPage, this.filteredAddons.length);
  }

  sortTable(column: keyof AddOnDto) {
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
    if (this.currentPage * this.itemsPerPage < this.addOns.length) {
      this.currentPage++;
      this.applyFilters();
    }
  }

  openAddEditModal(addon?: AddOnDto) {
    this.selectedAddOn = addon ? { ...addon } : null;
    this.form.reset();
    if (addon) {
      this.form.patchValue(addon);
    }
    this.showModal = true;
  }

  closeModal(result: AddOnDto | boolean) {
    this.showModal = false;
    if (typeof result !== 'boolean' && result) {
      // Handle saving data here
      this.save(result); // Pass the form data to save
    } else if (result === true) {
      this.load(); // Reload if needed
    }
  }

  // Modify save method to accept data:
  save(dto: AddOnDto) {
    this.loading = true;
    this.adminService.createOrUpdateAddOn(dto)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => this.load(),
        error: () => this.errorMsg = 'Failed to save add-on.'
      });
  }

  deleteAddOn(addon: AddOnDto) {
    if (!confirm(`Delete add-on "${addon.name}"?`)) return;
    this.loading = true;
    this.adminService.deleteAddOn(addon.addOnId)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => this.load(),
        error: () => { this.errorMsg = 'Failed to delete add-on.'; }
      });
  }

  exportCSV() {
    const headers = this.headers.map(h => h.label).join(',');
    const rows = this.filteredAddons.map(addon =>
      [addon.addOnId, addon.name, addon.description, addon.price].join(',')
    );
    const csv = [headers, ...rows].join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'addons.csv';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  ngDoCheck() {
    this.applyFilters();
  }
}
