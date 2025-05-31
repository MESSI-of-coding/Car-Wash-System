using System;

namespace CarWash.Domain.Models
{
    public class Leaderboard
    {
        public Guid EntryId { get; set; } // Primary key
        public Guid UserId { get; set; } // Foreign key to Users
        public int TotalWaterSavedGallons { get; set; } // Total water saved by the user
        public DateTime UpdatedAt { get; set; } // Timestamp of the last leaderboard update

        // Navigation property
        public User User { get; set; } = null!; // Reference to the User entity
    }
}