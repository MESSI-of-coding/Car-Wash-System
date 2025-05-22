//Path: CarWash/carwash-frontend/src/app/core/services/car.service.ts
// Purpose: This service handles operations related to cars, such as adding a new car and fetching cars associated with the logged-in user.

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Car } from '../models/car.model';
import { CarDto } from '../models/car.dto';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private baseUrl = `${environment.apiBaseUrl}/api/cars`;

  constructor(private http: HttpClient) {}

  addCar(carDto: CarDto): Observable<Car> {
    return this.http.post<Car>(`${this.baseUrl}`, carDto);
  }

  getCarsByUser(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.baseUrl}/user`); //Replace with actual endpoint
  }
}
