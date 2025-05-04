using System;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int RequestId { get; set; } // Foreign key to WashRequests

        [Required]
        public int WasherId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Rating between 1 and 5

        [Required]
        public string Comments { get; set; } // Comments provided in the review

        [Required]
        public DateTime ReviewDate { get; set; } // Date the review was created

        // Navigation properties
        public WashRequest WashRequest { get; set; } = null!;
    }
}