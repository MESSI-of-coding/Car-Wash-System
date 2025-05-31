namespace CarWash.Domain.Models
{
    public class Car
    {
        public Guid CarId { get; set; }
        public Guid UserId { get; set; }
        public required string Model { get; set; } // Required
        public required string LicensePlate { get; set; } // Required
        public string? ImageURL { get; set; } // Nullable if optional
        public User? User { get; set; } // Nullable navigation property
    }
}