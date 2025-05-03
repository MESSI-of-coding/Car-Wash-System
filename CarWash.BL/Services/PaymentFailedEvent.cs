namespace CarWash.BL.Services
{
    public class PaymentFailedEvent
    {
        public string PaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}