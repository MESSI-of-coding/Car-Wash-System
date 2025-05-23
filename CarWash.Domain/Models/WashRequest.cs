using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarWash.Domain.Models
{
    public class WashRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int? WasherId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int PackageId { get; set; }

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        public DateTime? ActualWashDateTime { get; set; }

        [Required]
        public WashStatus Status { get; set; } = WashStatus.Pending;

        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } // Add CreatedAt property

        // Navigation properties
        [ForeignKey("CustomerId")]
        public User? Customer { get; set; }

        [ForeignKey("WasherId")]
        public User? Washer { get; set; }

        [ForeignKey("CarId")]
        public Car? Car { get; set; }

        [ForeignKey("PackageId")]
        public WashPackage? Package { get; set; }

        public ICollection<WashRequestAddOn> WashRequestAddOns { get; set; } = new List<WashRequestAddOn>();
    }

    public enum WashStatus
    {
        Pending,
        Accepted,
        InProgress,
        Completed,
        Cancelled
    }
}