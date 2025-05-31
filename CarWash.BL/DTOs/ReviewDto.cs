using System;

namespace CarWash.BL.DTOs
{
    public class ReviewDto
    {

        public Guid ReviewId { get; set; }
        public Guid RequestId { get; set; }

        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}