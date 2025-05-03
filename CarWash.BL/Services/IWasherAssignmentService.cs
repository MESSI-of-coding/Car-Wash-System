using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface IWasherAssignmentService
    {
        Task AssignWasherAsync(int requestId);
    }
}