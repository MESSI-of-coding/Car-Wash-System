import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar.component';
import { FooterComponent } from './shared/footer.component';

// Define a constant for common imports
export const COMMON_IMPORTS = [RouterOutlet];

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [COMMON_IMPORTS, NavbarComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'carwash-frontend';
}
