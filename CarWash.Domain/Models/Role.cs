using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public required string RoleName { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}