using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(int userId, string message);
        Task SendReminderAsync(int userId, int requestId);
        Task SendReceiptAsync(int userId, int requestId);
    }
}