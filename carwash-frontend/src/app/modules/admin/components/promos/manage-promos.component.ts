import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';
import { finalize } from 'rxjs/operators';
import { PromoModalComponent } from './promo-modal.component';

export interface PromoDto {
  promoId: string;
  code: string;
  description: string;
  discount: number;
  expiryDate: string;
}

@Component({
  selector: 'app-manage-promos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, PromoModalComponent],
  templateUrl: './manage-promos.component.html'
})
export class ManagePromosComponent implements OnInit {
  promos: PromoDto[] = [];
  filteredPromos: PromoDto[] = [];
  loading = false;
  errorMsg = '';
  searchQuery = '';
  sortColumn: keyof PromoDto | '' = '';
  sortDirection: 'asc' | 'desc' = 'asc';
  currentPage = 1;
  itemsPerPage = 10;
  showModal = false;
  selectedPromo: PromoDto | null = null;
  form: FormGroup;
  headers = [
    { key: 'promoId', label: 'ID' },
    { key: 'code', label: 'Code' },
    { key: 'description', label: 'Description' },
    { key: 'discount', label: 'Discount (%)' },
    { key: 'expiryDate', label: 'Expiry Date' }
  ];

  constructor(private adminService: AdminService, private fb: FormBuilder) {
    this.form = this.fb.group({
      promoId: [null],
      code: ['', Validators.required],
      description: [''],
      discount: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
      expiryDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.errorMsg = '';
    this.adminService.getPromos()
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: res => {
          this.promos = res;
          this.applyFilters();
        },
        error: err => {
          this.errorMsg = 'Failed to load promos.';
        }
      });
  }

  applyFilters() {
    let data = [...this.promos];
    if (this.searchQuery) {
      const q = this.searchQuery.toLowerCase();
      data = data.filter(promo =>
        promo.code.toLowerCase().includes(q) ||
        (promo.description && promo.description.toLowerCase().includes(q))
      );
    }
    if (this.sortColumn) {
      data = data.sort((a, b) => {
        const aVal = a[this.sortColumn as keyof PromoDto];
        const bVal = b[this.sortColumn as keyof PromoDto];
        if (aVal == null || bVal == null) return 0;
        if (aVal < bVal) return this.sortDirection === 'asc' ? -1 : 1;
        if (aVal > bVal) return this.sortDirection === 'asc' ? 1 : -1;
        return 0;
      });
    }
    // Pagination
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.filteredPromos = data.slice(start, end);
  }

  get currentPageStart() {
    return (this.currentPage - 1) * this.itemsPerPage + 1;
  }
  get currentPageEnd() {
    return Math.min(this.currentPage * this.itemsPerPage, this.promos.length);
  }

  sortTable(column: string) {
    const typedColumn = column as keyof PromoDto;
    if (this.sortColumn === typedColumn) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = typedColumn;
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
    if (this.currentPage * this.itemsPerPage < this.promos.length) {
      this.currentPage++;
      this.applyFilters();
    }
  }

  openAddEditModal(promo?: PromoDto) {
    this.selectedPromo = promo ? { ...promo } : null;
    this.form.reset();
    if (promo) {
      this.form.patchValue(promo);
    }
    this.showModal = true;
  }

  closeModal(reload: boolean = false) {
    this.showModal = false;
    this.selectedPromo = null;
    this.form.reset();
    if (reload) this.load();
  }

  save() {
    if (this.form.invalid) return;
    this.loading = true;
    const dto = this.form.value as PromoDto;
    this.adminService.createOrUpdatePromo(dto)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => {
          this.closeModal(true);
        },
        error: () => {
          this.errorMsg = 'Failed to save promo.';
        }
      });
  }

  deletePromo(promo: PromoDto) {
    if (!confirm(`Delete promo "${promo.code}"?`)) return;
    this.loading = true;
    this.adminService.deletePromo(promo.promoId)
      .pipe(finalize(() => this.loading = false))
      .subscribe({
        next: () => this.load(),
        error: () => { this.errorMsg = 'Failed to delete promo.'; }
      });
  }

  exportCSV() {
    const headers = this.headers.map(h => h.label).join(',');
    const rows = this.filteredPromos.map(promo =>
      [promo.promoId, promo.code, promo.description, promo.discount, promo.expiryDate].join(',')
    );
    const csv = [headers, ...rows].join('\n');
    const blob = new Blob([csv], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'promos.csv';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  ngDoCheck() {
    this.applyFilters();
  }
}
