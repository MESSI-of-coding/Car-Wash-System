namespace CarWash.BL.DTOs
{
    public class CreateWashRequestDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int PackageId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public required LocationDto Location { get; set; } // Updated to use LocationDto
    }

    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}