using Stripe;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarWash.DAL.Data;
using CarWash.BL.Events; // Add this line

namespace CarWash.BL.Services
{
    public class PaymentCreationException : Exception
    {
        public PaymentCreationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class PaymentService
    {
        private readonly AppDbContext _context;
        private readonly IEventBus _eventBus;

        public PaymentService(AppDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100), // Convert to cents
                    Currency = currency,
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                // Publish PaymentCompleted event
                await _eventBus.PublishAsync(new PaymentCompleted
                {
                    Amount = amount,
                    Currency = currency
                });

                return paymentIntent.ClientSecret;
            }
            catch (StripeException ex)
            {
                throw new PaymentCreationException("Failed to create payment intent.", ex);
            }
        }

        public async Task<bool> ValidateUserOwnsRequestAsync(int userId, int requestId)
        {
            // Query database to check if the user owns the request
            return await _context.WashRequests
                .AnyAsync(wr => wr.RequestId == requestId && wr.CustomerId == userId);
        }
    }
}