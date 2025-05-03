using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}