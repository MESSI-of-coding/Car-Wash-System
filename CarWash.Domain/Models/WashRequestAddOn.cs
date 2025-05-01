using System.ComponentModel.DataAnnotations;

namespace CarWash.Domain.Models
{
    public class WashRequestAddOn
    {
        public int RequestId { get; set; }
        public int AddOnId { get; set; }
        public int Quantity { get; set; } = 1;

        // Navigation properties
        public required WashRequest WashRequest { get; set; }
        public required AddOn AddOn { get; set; }
    }
}