import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from 'src/app/shared/services/user.service';
import { ToastService } from 'src/app/shared/services/toast.service';
import { Router } from '@angular/router';

declare const google: any;

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  form!: FormGroup;
  isWasher: boolean = false;
  averageRating: number = 0;
  reviewCount: number = 0;

  map!: google.maps.Map;
  marker!: google.maps.Marker;
  defaultLatLng = { lat: 20.5937, lng: 78.9629 }; // fallback to India
  location: { lat: number; lng: number } = this.defaultLatLng;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private toast: ToastService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.getProfile().subscribe(user => {
      this.form = this.fb.group({
        fullName: [user.fullName, Validators.required],
        email: [user.email, [Validators.required, Validators.email]],
        location: this.fb.group({
          latitude: [user.location?.latitude || this.defaultLatLng.lat],
          longitude: [user.location?.longitude || this.defaultLatLng.lng]
        })
      });

      this.isWasher = user.roles.includes('Washer');
      if (this.isWasher) {
        this.userService.getWasherStats(user.userId).subscribe(stats => {
          this.averageRating = stats.averageRating;
          this.reviewCount = stats.totalReviews;
        });
      }

      // Set location for map
      this.location = {
        lat: user.location?.latitude || this.defaultLatLng.lat,
        lng: user.location?.longitude || this.defaultLatLng.lng
      };
      setTimeout(() => this.initMap(), 0);
    });
  }

  initMap() {
    const mapEl = document.getElementById('map');
    if (!mapEl) return;

    this.map = new google.maps.Map(mapEl, {
      center: this.location,
      zoom: 14
    });

    this.marker = new google.maps.Marker({
      position: this.location,
      map: this.map,
      draggable: true
    });

    this.marker.addListener('dragend', (event: any) => {
      this.location.lat = event.latLng.lat();
      this.location.lng = event.latLng.lng();
      // update form's location
      this.form.get('location')?.setValue({
        latitude: this.location.lat,
        longitude: this.location.lng
      });
    });
  }

  save(): void {
    if (this.form.invalid) return;
    this.userService.updateProfile(this.form.value).subscribe({
      next: () => this.toast.success('Profile updated!'),
      error: () => this.toast.error('Update failed')
    });
  }
}
