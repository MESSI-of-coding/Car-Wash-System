using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string message);
        Task SendReminderAsync(Guid userId, Guid requestId);
        Task SendReceiptAsync(Guid userId, Guid requestId);
    }
}