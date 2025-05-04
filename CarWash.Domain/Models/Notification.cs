using System;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // Matches ENUM('StatusUpdate', 'Payment', 'Reminder')
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } // Navigation property to User
    }
}