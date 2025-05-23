export interface User {
  userId: number;
  email: string;
  passwordHash: string;
  fullName?: string;
  contactNumber?: string;
  createdAt: Date;
  location: {
    latitude: number;
    longitude: number;
  };
  isActive: boolean;

  // Optional navigation properties
  userRoles?: any[]; // Define a UserRole interface if needed
  cars?: any[];      // Optional: can use Car[] if circular ref is handled
  notifications?: any[]; // Define Notification interface later
}
