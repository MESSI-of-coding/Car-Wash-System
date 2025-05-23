export interface RegisterRequest {
  fullName: string; // Changed from 'name' to 'fullName' to match backend DTO
  email: string;
  password: string;
  contactNumber?: string;
  location: {
    latitude: number;
    longitude: number;
  };
  role: 'Customer' | 'Washer';        // Role selection from dropdown
}
