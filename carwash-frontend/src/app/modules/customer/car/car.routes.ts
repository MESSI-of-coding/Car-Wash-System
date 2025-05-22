import { Routes } from '@angular/router';


export const routes: Routes = [
  {
    path: 'add',
    loadComponent: () =>
      import('./components/add-car.component').then(m => m.AddCarComponent)
  },
  {
    path: 'list',
    loadComponent: () =>
      import('./list-cars.component').then(m => m.ListCarsComponent)
  }
];