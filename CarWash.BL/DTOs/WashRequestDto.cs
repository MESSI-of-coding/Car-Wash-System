using System;

namespace CarWash.BL.DTOs
{
    public class WashRequestDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public int PackageId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string? Notes { get; set; }
    }
}