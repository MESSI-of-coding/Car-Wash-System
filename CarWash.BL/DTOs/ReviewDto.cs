using System;

namespace CarWash.BL.DTOs
{
    public class ReviewDto
    {

        public int ReviewId { get; set; }
        public int RequestId { get; set; }

        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}