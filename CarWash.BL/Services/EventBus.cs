using System;
using System.Threading.Tasks;

namespace CarWash.BL.Services
{
    public class EventBus : IEventBus
    {
        public Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
        {
            // Add your event publishing logic
            return Task.CompletedTask;
        }

        public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : class
        {
            // Add subscription logic
        }
    }
}