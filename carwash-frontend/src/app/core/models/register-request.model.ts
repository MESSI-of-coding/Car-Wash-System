export interface RegisterRequest {
  email: string;
  password: string;
  fullName?: string;
  contactNumber?: string;
  location: {
    type: 'Point';                    // Fixed to "Point" for GeoJSON
    coordinates: [number, number];    // [longitude, latitude]
  };
  role: 'Customer' | 'Washer';        // Role selection from dropdown
}
