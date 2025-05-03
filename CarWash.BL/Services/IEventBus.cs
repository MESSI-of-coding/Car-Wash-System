using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class;
        void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : class;
    }
}