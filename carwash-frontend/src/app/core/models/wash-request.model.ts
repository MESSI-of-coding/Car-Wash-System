export interface WashRequestDto {
  carId: number;
  packageId: number;
  addOns: string[];
  isImmediate: boolean;
  scheduleDateTime?: string | null;
  notes?: string; // Optional notes field
}

export interface WashRequest {
  requestId: number;
  customerId: number;
  washerId?: number; // Nullable washer ID
  car: { carId: number; model: string; licensePlate: string; imageURL?: string };
  package: { packageId: number; packageName: string; description?: string; price: number };
  addOns: string[];
  status: 'Pending' | 'Accepted' | 'InProgress' | 'Completed' | 'Cancelled';
  scheduledDateTime: string; // ISO string for ScheduledDateTime
  actualWashDateTime?: string; // Nullable ISO string for ActualWashDateTime
  createdAt: string; // ISO string for CreatedAt
  notes?: string; // Optional notes field
}