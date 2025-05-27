/// <reference types="@types/google.maps" />

import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WasherService } from '../../services/washer.service';
import { GoogleMapsLoaderService } from 'src/app/shared/services/google-maps-loader.service';

@Component({
  selector: 'app-washer-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class WasherProfileComponent implements OnInit, AfterViewInit {
  profile: any = {};
  imageFile: File | null = null;
  previewUrl: string = '';
  map!: google.maps.Map;
  marker!: google.maps.Marker;
  isSaving = false;

  constructor(private washerService: WasherService, private mapsLoader: GoogleMapsLoaderService) {}

  ngOnInit(): void {
    this.washerService.getProfile?.().subscribe((data: any) => {
      this.profile = data;
      this.previewUrl = data.imageUrl;
    });
  }

  async ngAfterViewInit() {
    await this.mapsLoader.load();

    // Use type-safe initialization
    navigator.geolocation.getCurrentPosition((pos) => {
      const lat = this.profile.latitude || pos.coords.latitude;
      const lng = this.profile.longitude || pos.coords.longitude;

      const mapEl = document.getElementById('map');
      if (!mapEl) return;

      this.map = new google.maps.Map(mapEl, {
        center: { lat, lng },
        zoom: 14
      });

      this.marker = new google.maps.Marker({
        position: { lat, lng },
        map: this.map,
        draggable: true
      });

      // Add typed event listener
      this.marker.addListener('dragend', () => {
        const position = this.marker.getPosition();
        if (position) {
          this.profile.latitude = position.lat();
          this.profile.longitude = position.lng();
        }
      });
    });
  }

  onFileChange(event: any): void {
    this.imageFile = event.target.files[0];
    if (!this.imageFile) return;
    const reader = new FileReader();
    reader.onload = () => (this.previewUrl = reader.result as string);
    reader.readAsDataURL(this.imageFile);
  }

  save(): void {
    this.isSaving = true;
    const formData = new FormData();
    formData.append('fullName', this.profile.fullName);
    formData.append('email', this.profile.email);
    formData.append('contactNumber', this.profile.contactNumber);
    formData.append('latitude', this.profile.latitude?.toString() ?? '');
    formData.append('longitude', this.profile.longitude?.toString() ?? '');
    if (this.imageFile) {
      formData.append('profileImage', this.imageFile);
    }
    this.washerService.updateProfile?.(formData).subscribe({
      next: () => {
        this.isSaving = false;
        alert('Profile updated successfully!');
      },
      error: () => {
        this.isSaving = false;
        alert('Failed to update profile.');
      }
    });
  }
}
