//Path: CarWash/carwash-frontend/src/app/modules/auth/login.component.ts
//Purpose: This code defines a LoginComponent in Angular that handles user login functionality. 
//It uses Reactive Forms for form handling and validation, and it interacts with an AuthService to perform the login operation. 
//Upon successful login, it stores the JWT token in local storage and redirects the user based on their role (admin, washer, or customer).

/// <reference types="@types/google.maps" />

import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, AbstractControl, ValidationErrors } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service'; 
import { ToastService } from 'src/app/shared/services/toast.service';

declare var google: any;

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;
  roles = ['Customer', 'Washer'];
  locationError = '';
  isLocating = false;

  @ViewChild('locationSearch') locationSearch!: ElementRef;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toast: ToastService
  ) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), this.passwordStrengthValidator]],
      fullName: ['', Validators.required],
      contactNumber: [''],
      role: ['Customer', Validators.required],
      location: this.fb.group({
        latitude: [0, Validators.required],
        longitude: [0, Validators.required]
      })
    });
  }

  ngAfterViewInit() {
    // Initialize Google Places Autocomplete
    if (typeof google !== 'undefined') {
      const autocomplete = new google.maps.places.Autocomplete(
        this.locationSearch.nativeElement
      );
      
      autocomplete.addListener('place_changed', () => {
        const place = autocomplete.getPlace();
        if (place.geometry) {
          this.registerForm.patchValue({
            location: {
              coordinates: [
                place.geometry.location.lng(),
                place.geometry.location.lat()
              ]
            }
          });
          this.locationError = '';
        }
      });
    }
  }

  async getLocation() {
    this.isLocating = true;
    this.locationError = '';
    
    if (!navigator.geolocation) {
      this.locationError = 'Geolocation is not supported by your browser';
      this.isLocating = false;
      return;
    }

    try {
      const position = await new Promise<GeolocationPosition>((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(resolve, reject, {
          enableHighAccuracy: true,
          timeout: 10000
        });
      });

      this.registerForm.patchValue({
        location: {
          coordinates: [
            position.coords.longitude,
            position.coords.latitude
          ]
        }
      });
      
    } catch (error: any) {
      this.locationError = 'Unable to retrieve your location. Please enable permissions or enter manually.';
      console.error('Geolocation error:', error);
    } finally {
      this.isLocating = false;
    }
  }

  passwordStrengthValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value;
    const strongRegex = /^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9]).{6,}$/;
    return strongRegex.test(value) ? null : { weakPassword: true };
  }

  validCoordinates(control: AbstractControl): ValidationErrors | null {
    const [lng, lat] = control.value;
    return (Math.abs(lat) <= 90 && Math.abs(lng) <= 180) ? null : { invalidCoords: true };
  }

  onSubmit() {
    if (this.registerForm.invalid) return;

    const formValue = this.registerForm.value;
    const userData = {
      email: formValue.email,
      password: formValue.password,
      fullName: formValue.fullName,
      contactNumber: formValue.contactNumber,
      role: formValue.role,
      location: formValue.location
    };

    this.authService.register(userData).subscribe({
      next: () => {
        this.toast.show('Registration successful');
        this.router.navigate(['/login']);
      },
      error: err => this.toast.show('Registration failed: ' + (err.error?.message || err.statusText))
    });
  }
}