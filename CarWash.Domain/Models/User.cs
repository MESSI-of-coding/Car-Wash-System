using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace CarWash.Domain.Models
{
    public class User
    {
        // Initialize collections to avoid nulls
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Cars = new HashSet<Car>();
        }

        public int UserId { get; set; }

        // Add 'required' to enforce non-null
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? FullName { get; set; } // Nullable if optional
        public string? ContactNumber { get; set; } // Nullable if optional
        public DateTime CreatedAt { get; set; }
        public required Point Location { get; set; } // Maps to GEOMETRY in SQL
        public bool IsActive { get; set; }

        // Initialized collections
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}