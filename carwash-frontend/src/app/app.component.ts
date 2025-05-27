import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { ToastComponent } from './shared/components/toast/toast.component';
import { GoogleMapsLoaderService } from './shared/services/google-maps-loader.service';
import { HomeComponent } from './features/home/home.component';

// Define a constant for common imports
export const COMMON_IMPORTS = [RouterOutlet];

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [COMMON_IMPORTS, NavbarComponent, FooterComponent, ToastComponent, HomeComponent, RouterOutlet],
  // template: `<app-home></app-home>`,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  //title = 'BrightWheel Care';

  // constructor(private googleMapsLoader: GoogleMapsLoaderService) {}

  // async ngOnInit() {
  //   await this.googleMapsLoader.load();
  // }
}
