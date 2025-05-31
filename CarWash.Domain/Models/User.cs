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
            Notifications = new HashSet<Notification>();
            UserId = Guid.NewGuid();

        }

        public Guid UserId { get; set; }

        // Add 'required' to enforce non-null
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? FullName { get; set; } // Nullable if optional
        public string? ContactNumber { get; set; } // Nullable if optional
        public DateTime CreatedAt { get; set; }
        public required Point Location { get; set; } // Maps to GEOMETRY in SQL
        // For DTO mapping: expose as { latitude = Location.Y, longitude = Location.X }
        public bool IsActive { get; set; }

        // Initialized collections
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>(); // Navigation property for Notifications
    }
}