import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/shared/services/toast.service';
import { WashRequestService } from 'src/app/core/services/wash-request.service'; // Adjust the import path as necessary
import { CarService } from 'src/app/core/services/car.service'; // Adjust the import path as necessary
import { WashService } from 'src/app/shared/services/wash.service'; // Adjust the import path as necessary
import { Car } from 'src/app/core/models/car.model'; // Adjust the import path as necessary
import { WashPackage } from 'src/app/core/models/wash-package.model'; // Adjust the import path as necessary

@Component({
  selector: 'app-wash-now',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './wash-now.component.html',
  styleUrl: './wash-now.component.scss'
})
export class WashNowComponent {
  form: FormGroup;
  cars: Car[] = [];
  packages: WashPackage[] = [];
  addOns: string[] = ['Wax', 'Interior Cleaning', 'Engine Wash'];

  constructor(
    private fb: FormBuilder,
    private carService: CarService,
    private washService: WashService,
    private router: Router,
    private toastService: ToastService,
    private washRequestService: WashRequestService
  ) {
    this.form = this.fb.group({
      carId: ['', Validators.required],
      packageId: ['', Validators.required],
      addOns: [[]],
      isImmediate: [true],
      scheduleDate: [''],
      scheduleTime: [''],
      notes: [''] // Add notes field
    });
  }

  ngOnInit(): void {
    this.carService.getCarsByUser().subscribe(data => this.cars = data);
    this.washService.getPackages().subscribe(data => this.packages = data);
  }

  loadCars(): void {
    this.carService.getCarsByUser().subscribe({
      next: (cars) => {
        this.cars = cars;
        this.form.patchValue({ carId: cars[0]?.carId }); // Set default car
      },
      error: () => {
        this.toastService.show('Failed to load cars', 'error');
      }
    });
  }

  loadPackages(): void {
    this.washService.getPackages().subscribe({
      next: (packages) => {
        this.packages = packages;
        this.form.patchValue({ packageId: packages[0]?._id }); // Set default package
      },
      error: () => {
        this.toastService.show('Failed to load wash packages', 'error');
      }
    });
  }

  toggleAddOn(addon: string, event: Event): void {
    const addOnsControl = this.form.get('addOns');
    if (!addOnsControl) return;
    const addOns = addOnsControl.value as string[];
    if ((event.target as HTMLInputElement).checked) {
      if (!addOns.includes(addon)) {
        addOns.push(addon);
      }
    } else {
      const idx = addOns.indexOf(addon);
      if (idx > -1) {
        addOns.splice(idx, 1);
      }
    }
    addOnsControl.setValue([...addOns]);
    addOnsControl.markAsDirty();
  }

  onSubmit(): void {
    if (!this.form.valid) return;

    // Combine scheduleDate and scheduleTime into ScheduledDateTime
    const { scheduleDate, scheduleTime, ...rest } = this.form.value;
    const scheduledDateTime = `${scheduleDate}T${scheduleTime}`;

    const washRequest = {
      ...rest,
      scheduledDateTime,
      notes: this.form.get('notes')?.value || '', // Add notes if available
    };

    // Call the service to submit the wash request
    this.washRequestService.createWashRequest(washRequest).subscribe({
      next: () => {
        this.toastService.show('Wash request created successfully', 'success');
        this.router.navigate(['/customer/wash/requests']);
      },
      error: () => {
        this.toastService.show('Failed to create wash request', 'error');
      }
    });
  }
}
