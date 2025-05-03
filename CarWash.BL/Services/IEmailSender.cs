using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}