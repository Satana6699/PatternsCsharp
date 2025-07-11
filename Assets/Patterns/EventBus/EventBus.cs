using System;
using System.Collections.Generic;

namespace App.Scripts.Infrastructure.Services.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _subscribers;
        private readonly object _lock = new object();

        public EventBus()
        {
            _subscribers = new Dictionary<Type, Delegate>();
        }
        
        public void Publish<TEvent>(TEvent eventData) where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);
            if (!_subscribers.TryGetValue(eventType, out var handlers))
            {
                return;
            }

            (handlers as Action<TEvent>)?.Invoke(eventData);
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Type eventType = typeof(TEvent);
            
            lock (_lock)
            {
                if (_subscribers.TryGetValue(eventType, out var handlers))
                {
                    _subscribers[eventType] = Delegate.Combine(handlers, handler);
                }
                else
                {
                    _subscribers[eventType] = handler;
                }
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Type eventType = typeof(TEvent);
            
            lock (_lock)
            {
                if (!_subscribers.TryGetValue(eventType, out var handlers))
                {
                    return;
                }

                var updatedHandlers = Delegate.Remove(handlers, handler);

                if (updatedHandlers == null)
                {
                    _subscribers.Remove(eventType);
                }
                else
                {
                    _subscribers[eventType] = updatedHandlers;
                }
            }
        }
        
        public void UnsubscribeAll<TEvent>() where TEvent : IEvent
        {
            Type eventType = typeof(TEvent);
            
            lock (_lock)
            {
                _subscribers.Remove(eventType);
            }
        }
    }
}