using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Add email sending logic
            return Task.CompletedTask;
        }
    }
}