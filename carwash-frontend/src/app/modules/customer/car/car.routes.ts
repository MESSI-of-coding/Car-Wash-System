import { Routes } from '@angular/router';


export const routes: Routes = [
  {
    path: 'add',
    loadComponent: () =>
      import('./components/add-car/add-car.component').then(m => m.AddCarComponent)
  },
  {
    path: 'list',
    loadComponent: () =>
      import('./components/car-list/car-list.component').then(m => m.CarListComponent)
  }
];