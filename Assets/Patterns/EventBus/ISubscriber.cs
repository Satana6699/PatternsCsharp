using System;

namespace App.Scripts.Infrastructure.Services.EventBus
{
    public interface ISubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
        void UnsubscribeAll<TEvent>() where TEvent : IEvent;
    }
}