namespace CarWash.BL.Services
{
    public interface IPaymentService
    {
        Task UpdatePaymentStatusAsync(string paymentId, string status);
        Task<string> CreatePaymentIntentAsync(decimal amount, string currency); //Create a payment intent for the specified amount and currency.
        Task<bool> ValidateUserOwnsRequestAsync(Guid userId, Guid requestId); // Check if the user owns the request.
    }
}