import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { COMMON_IMPORTS } from './common-imports';

@Component({
  selector: 'app-footer',
  standalone: true,
  imports: [CommonModule, COMMON_IMPORTS],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {

}
