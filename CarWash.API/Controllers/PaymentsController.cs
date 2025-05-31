using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Events;
using System.IO;
using System.Threading.Tasks;
using CarWash.BL.Services;
using System.Security.Claims;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private const string StripeSignatureHeader = "Stripe-Signature";
        private readonly string _endpointSecret = "your_endpoint_secret_here"; // Replace with your actual endpoint secret
        private readonly IPaymentService _paymentService;
        private readonly IEventPublisher _eventPublisher;

        public PaymentsController(IPaymentService paymentService, IEventPublisher eventPublisher)
        {
            _paymentService = paymentService;
            _eventPublisher = eventPublisher;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers[StripeSignatureHeader],
                    _endpointSecret
                );

                // Replace Stripe event constants with raw string equivalents
                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    // Handle successful payment
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent != null)
                    {
                        await _paymentService.UpdatePaymentStatusAsync(paymentIntent.Id, "Succeeded");
                        await _eventPublisher.PublishAsync(new PaymentCompletedEvent
                        {
                            PaymentId = paymentIntent.Id,
                            Amount = paymentIntent.Amount / 100m,
                            Currency = paymentIntent.Currency
                        });
                    }
                }
                else if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    // Handle failed payment
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (paymentIntent != null)
                    {
                        await _paymentService.UpdatePaymentStatusAsync(paymentIntent.Id, "Failed");
                        await _eventPublisher.PublishAsync(new PaymentFailedEvent
                        {
                            PaymentId = paymentIntent.Id,
                            Amount = paymentIntent.Amount / 100m,
                            Currency = paymentIntent.Currency
                        });
                    }
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{requestId}/client-secret")]
        public async Task<IActionResult> GetClientSecret(Guid requestId)
        {
            // Validate that the calling user owns the requestId
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID is missing in the token.");
            }

            var userId = Guid.Parse(userIdClaim);

            // Replace with actual logic to validate ownership of the requestId
            var ownsRequest = await _paymentService.ValidateUserOwnsRequestAsync(userId, requestId);
            if (!ownsRequest)
            {
                return Forbid("You do not own this request.");
            }

            // Fetch the wash request amount (mocked for now, replace with actual logic)
            decimal washRequestAmount = 100.00m; // Replace with actual logic to fetch the amount

            // Call PaymentService to create a payment intent
            var clientSecret = await _paymentService.CreatePaymentIntentAsync(washRequestAmount, "usd");

            // Return the client secret in JSON
            return Ok(new { clientSecret });
        }
    }
}