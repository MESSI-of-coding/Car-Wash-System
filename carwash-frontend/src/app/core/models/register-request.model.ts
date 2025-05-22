export interface RegisterRequest {
  fullName: string; // Changed from 'name' to 'fullName' to match backend DTO
  email: string;
  password: string;
  contactNumber?: string;
  location: {
    type: 'Point';                    // Fixed to "Point" for GeoJSON
    coordinates: [number, number];    // [longitude, latitude]
  };
  role: 'Customer' | 'Washer';        // Role selection from dropdown
}
