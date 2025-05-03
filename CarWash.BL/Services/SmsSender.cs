using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(string phoneNumber, string message)
        {
            // Add SMS sending logic here
            return Task.CompletedTask;
        }
    }
}