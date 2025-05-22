import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CarService } from 'src/app/core/services/car.service';


@Component({
  selector: 'app-car-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.scss'
})
export class CarListComponent implements OnInit {
  cars: any[] = [];

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.loadUserCars();
  }

  loadUserCars(): void {
    this.carService.getCarsByUser().subscribe({
      next: (response: any) => this.cars = response,
      error: (err) => console.error('Failed to load cars', err)
    });
  }
}
