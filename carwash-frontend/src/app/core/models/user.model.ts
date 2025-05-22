export interface User {
  userId: number;
  email: string;
  passwordHash: string;
  fullName?: string;
  contactNumber?: string;
  createdAt: Date;
  location: {
    type: string;        // "Point"
    coordinates: [number, number]; // [longitude, latitude]
  };
  isActive: boolean;

  // Optional navigation properties
  userRoles?: any[]; // Define a UserRole interface if needed
  cars?: any[];      // Optional: can use Car[] if circular ref is handled
  notifications?: any[]; // Define Notification interface later
}
