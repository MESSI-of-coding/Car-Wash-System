// Model for a car wash package
export interface WashPackage {
  packageId: number;
  packageName: string;
  description?: string;
  price: number;
  durationMinutes?: number; // Optional: estimated duration
  isActive: boolean;
}
