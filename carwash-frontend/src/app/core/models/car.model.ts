import { User } from './user.model';

export interface Car {
  carId: string;
  userId: string;
  model: string;
  licensePlate: string;
  imageURL?: string; // optional
  user?: User; // Optional navigation property
}