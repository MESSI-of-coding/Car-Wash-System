// Model for a car wash package
export interface WashPackage {
  packageId: string;
  packageName: string;
  description?: string;
  price: number;
  durationMinutes?: number; // Optional: estimated duration
  isActive: boolean;
  waterSavedGallons: number; 
}
