import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgxChartsModule, Color, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-reports-dashboard',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgxChartsModule],
  templateUrl: './reports-dashboard.component.html',
  styleUrl: './reports-dashboard.component.scss'
})
export class ReportsDashboardComponent {
  form: FormGroup;
  isGenerating = false;
  stats: any = null;
  showChart = false;
  chartData: any[] = [];
  colorScheme: Color = {
    name: 'custom',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#1e88e5', '#43a047', '#fbc02d', '#e53935', '#8e24aa', '#00acc1']
  };

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      from: ['', Validators.required],
      to: ['', Validators.required],
      type: ['All', Validators.required]
    }, { validators: this.dateOrderValidator });
  }

  dateOrderValidator(group: FormGroup) {
    const from = group.get('from')?.value;
    const to = group.get('to')?.value;
    return from && to && from > to ? { dateOrder: true } : null;
  }

  generate() {
    if (this.form.invalid) return;
    this.isGenerating = true;
    // Simulate API call
    setTimeout(() => {
      this.stats = { total: 100 };
      this.chartData = [
        { name: 'Basic Wash', value: 40 },
        { name: 'Deluxe Package', value: 35 },
        { name: 'Premium Detail', value: 25 }
      ];
      this.showChart = true;
      this.isGenerating = false;
    }, 1200);
  }

  download() {
    // Simulate download
    alert('Export to Excel triggered!');
  }
}
