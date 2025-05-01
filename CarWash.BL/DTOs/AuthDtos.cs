namespace CarWash.BL.DTOs
{
    public class RegisterDto
    {
        public required string Email { get; set; } // Required email for registration
        public required string Password { get; set; } // Required password for registration
        public string? FullName { get; set; } // Optional full name
        public string? ContactNumber { get; set; } // Optional contact number
    }

    public class LoginDto
    {
        public required string Email { get; set; } // Required email for login
        public required string Password { get; set; } // Required password for login
    }
}