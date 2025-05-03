using System;
using System.ComponentModel.DataAnnotations; // Add for validation attributes

namespace CarWash.BL.DTOs
{
    public class WashRequestDto
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "CarId is required.")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "PackageId is required.")]
        public int PackageId { get; set; }

        [Required(ErrorMessage = "ScheduledDateTime is required.")]
        [Range(typeof(DateTime), "2025-05-03", "2099-12-31", ErrorMessage = "ScheduledDateTime must be in the future.")]
        public DateTime ScheduledDateTime { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public required string Location { get; set; } // Add "required" to enforce non-nullable property
    }
}