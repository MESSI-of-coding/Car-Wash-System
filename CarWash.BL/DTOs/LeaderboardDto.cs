using System;

namespace CarWash.BL.DTOs
{
    public class LeaderboardDto
    {
        public int UserId { get; set; } // User ID
        public int TotalWaterSavedGallons { get; set; } // Total water saved
        public DateTime UpdatedAt { get; set; } // Last updated timestamp
    }
}