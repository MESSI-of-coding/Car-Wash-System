namespace CarWash.BL.DTOs
{
    public class CarDto
    {
        public int CarId { get; set; }
        public required string Model { get; set; } // Add required
        public required string LicensePlate { get; set; } // Add required
    }
}