import { Component } from '@angular/core';
import { COMMON_IMPORTS } from './common-imports';

@Component({
  selector: 'app-loading-spinner',
  standalone: true,
  imports: [COMMON_IMPORTS],
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.scss'
})
export class LoadingSpinnerComponent {

}
