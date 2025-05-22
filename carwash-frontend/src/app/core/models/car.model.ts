import { User } from './user.model';

export interface Car {
  carId: number;
  userId: number;
  model: string;
  licensePlate: string;
  imageURL?: string; // optional
  user?: User; // Optional navigation property
}