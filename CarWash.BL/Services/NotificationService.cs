using System;
using System.Threading.Tasks;
using CarWash.BL.Events;
using CarWash.DAL.Data;
using CarWash.Domain.Models;

namespace CarWash.BL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        // Subscribe to events in the constructor
        public NotificationService(AppDbContext dbContext, IEmailSender emailSender, ISmsSender smsSender, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _smsSender = smsSender;

            // Subscribe to events
            eventBus.Subscribe<RequestCreated>(async e => await SendNotificationAsync(e.UserId, "Your request has been created."));
            eventBus.Subscribe<RequestAccepted>(async e => await SendNotificationAsync(e.UserId, "Your request has been accepted."));
            eventBus.Subscribe<WashRequestAssigned>(async e => await SendNotificationAsync(e.UserId, "A washer has been assigned to your request."));
            eventBus.Subscribe<PaymentCompleted>(async e => await SendNotificationAsync(e.UserId, "Your payment has been completed."));
            eventBus.Subscribe<WashRequestCompleted>(async e => await SendNotificationAsync(e.UserId, "Your wash request has been completed."));
        }

        public async Task SendNotificationAsync(Guid userId, string message)
        {
            // Create a new Notification record
            var notification = new Notification
            {
                UserId = userId,
                Message = message
                // Removed CreatedAt assignment as it is no longer part of the model
            };

            // Save to the database
            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();

            // Dispatch the notification (e.g., via email or SMS)
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    await _emailSender.SendEmailAsync(user.Email, "Notification", message);
                }
                else if (!string.IsNullOrEmpty(user.ContactNumber))
                {
                    await _smsSender.SendSmsAsync(user.ContactNumber, message);
                }
            }
        }

        public async Task SendReminderAsync(Guid userId, Guid requestId)
        {
            var message = $"Reminder: Your wash request #{requestId} is scheduled.";
            await SendNotificationAsync(userId, message);
        }

        public async Task SendReceiptAsync(Guid userId, Guid requestId)
        {
            var message = $"Receipt: Your wash request #{requestId} has been completed. Thank you!";
            await SendNotificationAsync(userId, message);
        }
    }
}