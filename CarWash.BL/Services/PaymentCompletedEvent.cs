namespace CarWash.BL.Services
{
    public class PaymentCompletedEvent
    {
        public string PaymentId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}