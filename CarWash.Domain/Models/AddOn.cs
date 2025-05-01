using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class AddOn
    {
        [Key]
        public int AddOnId { get; set; }

        [Required]
        public required string AddOnName { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}