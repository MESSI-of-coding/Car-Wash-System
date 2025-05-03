namespace CarWash.BL.DTOs
{
    public class CreateWashRequestDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int PackageId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public required string Location { get; set; } // Add required
    }
}