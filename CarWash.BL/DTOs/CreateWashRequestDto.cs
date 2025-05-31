namespace CarWash.BL.DTOs
{
    public class CreateWashRequestDto
    {
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }
        public Guid PackageId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public required LocationDto Location { get; set; } // Updated to use LocationDto
    }
}