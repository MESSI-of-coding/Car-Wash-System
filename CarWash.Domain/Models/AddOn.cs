using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class AddOn
    {
        [Key]
        public Guid AddOnId { get; set; }

        [Required(ErrorMessage = "AddOnName is required.")]
        [StringLength(100, ErrorMessage = "AddOnName cannot exceed 100 characters.")]
        public required string AddOnName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}