namespace CarWash.BL.Events
{
    public class RequestCreated
    {
        public int UserId { get; set; }
        public int RequestId { get; set; }
    }

    public class RequestAccepted
    {
        public int UserId { get; set; }
        public int RequestId { get; set; }
    }

    public class WashRequestAssigned
    {
        public int UserId { get; set; }
        public int RequestId { get; set; }
    }

    public class PaymentCompleted
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public required string Currency { get; set; } // Add required
    }

    public class WashRequestCompleted
    {
        public int UserId { get; set; }
        public int RequestId { get; set; }
    }
}