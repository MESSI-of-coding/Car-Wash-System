namespace CarWash.BL.Events
{
    public class RequestCreated
    {
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }

    public class RequestAccepted
    {
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }

    public class WashRequestAssigned
    {
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }

    public class PaymentCompleted
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public required string Currency { get; set; } // Add required
    }

    public class WashRequestCompleted
    {
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
    }
}