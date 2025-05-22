export interface LoginResponse {
  token: string;          // JWT token
  expiresIn?: number;     // Optional: expiration timestamp in seconds
  role: string;           // e.g., "Customer", "Washer", "Admin"
}
