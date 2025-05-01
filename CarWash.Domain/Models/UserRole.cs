using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public required User User { get; set; }
        public required Role Role { get; set; }
    }
}