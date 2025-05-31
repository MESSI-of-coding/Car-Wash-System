using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class WashPackage
    {
        [Key]
        public Guid PackageId { get; set; }

        [Required]
        public required string PackageName { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int WaterSavedGallons { get; set; }
    }
}