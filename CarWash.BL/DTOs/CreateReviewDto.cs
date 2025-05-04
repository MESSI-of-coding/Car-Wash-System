using System.ComponentModel.DataAnnotations;

namespace CarWash.BL.DTOs
{
    public class CreateReviewDto
    {
        public int RequestId { get; set; } // Renamed from WashRequestId

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; } // Rename from Comment
    }
}