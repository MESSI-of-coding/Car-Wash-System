using System;
using System.ComponentModel.DataAnnotations; // Add for validation attributes

namespace CarWash.BL.DTOs
{
    public class WashRequestDto
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "CarId is required.")]
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "PackageId is required.")]
        public Guid PackageId { get; set; }

        [Required(ErrorMessage = "ScheduledDateTime is required.")]
        [Range(typeof(DateTime), "2025-05-03", "2099-12-31", ErrorMessage = "ScheduledDateTime must be in the future.")]
        public DateTime ScheduledDateTime { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public required LocationDto Location { get; set; } // Updated to use LocationDto
    }
}