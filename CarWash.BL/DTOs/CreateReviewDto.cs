using System.ComponentModel.DataAnnotations;

namespace CarWash.BL.DTOs
{
    public class CreateReviewDto
    {
        public Guid RequestId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }
    }
}