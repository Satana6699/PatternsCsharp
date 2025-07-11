namespace App.Scripts.Infrastructure.Services.EventBus
{
    public interface IPublisher 
    {
        void Publish<TEvent>(TEvent eventData) where TEvent : IEvent;
    }
}