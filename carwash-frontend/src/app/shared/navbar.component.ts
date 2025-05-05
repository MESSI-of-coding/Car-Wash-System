import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { COMMON_IMPORTS } from './common-imports';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, COMMON_IMPORTS],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {

}
