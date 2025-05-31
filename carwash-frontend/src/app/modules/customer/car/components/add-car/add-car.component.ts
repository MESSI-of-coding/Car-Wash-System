//Path - CarWash/carwash-frontend/src/app/modules/customer/car/components/add-car.component.ts

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CarService } from 'src/app/core/services/car.service'; 
import { ToastService } from 'src/app/shared/services/toast.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.scss'
})
export class AddCarComponent {
  carForm: FormGroup;
  selectedImage: string | ArrayBuffer | null = null;
  uploadedFile: File | null = null;
  fileError: string | null = null;
  isSubmitting = false;

  constructor(private fb: FormBuilder, private carService: CarService, private toastService: ToastService, private router: Router) {
    this.carForm = this.fb.group({
      model: ['', Validators.required],
      licensePlate: ['', Validators.required],
      image: [null]
    });
  }

  get selectedFile(): File | null {
    return this.uploadedFile;
  }

  removeImage(): void {
    this.selectedImage = null;
    this.uploadedFile = null;
    this.fileError = null;
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    this.uploadedFile = input.files[0];
    this.fileError = null;

    // Optional: Validate file type and size
    const allowedTypes = ['image/jpeg', 'image/png'];
    if (!allowedTypes.includes(this.uploadedFile.type)) {
      this.fileError = 'Only JPEG and PNG files are allowed.';
      this.uploadedFile = null;
      this.selectedImage = null;
      return;
    }
    if (this.uploadedFile.size > 5 * 1024 * 1024) {
      this.fileError = 'File size must be less than 5MB.';
      this.uploadedFile = null;
      this.selectedImage = null;
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      this.selectedImage = reader.result;
    };
    reader.readAsDataURL(this.uploadedFile);
  }

  onSubmit(): void {
    if (!this.carForm.valid) return;
    this.isSubmitting = true;
    const carDto = {
      carId: '', // Default or placeholder value for carId
      model: this.carForm.get('model')?.value,
      licensePlate: this.carForm.get('licensePlate')?.value,
      imageUrl: this.selectedImage || '', // if you're storing image as URL
    };

    this.carService.addCar(carDto).subscribe({
      next: () => {
        this.toastService.show('Car added successfully', 'success');
        this.router.navigate(['/customer/cars/list']);
        this.isSubmitting = false;
      },
      error: () => {
        this.toastService.show('Failed to add car', 'error');
        this.isSubmitting = false;
      }
    });
  }
}
