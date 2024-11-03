using MessagePipe;
using System;
using System.Collections.Generic;

namespace Strangeman.Message
{
    public class GlobalMessagePublisher
    {
        private readonly Dictionary<Type, object> _publishers = new();

        public void Publish<T>(T toPublish) where T : notnull
        {
            if (!_publishers.TryGetValue(typeof(T), out var publisher))
            {
                publisher = GlobalMessagePipe.GetPublisher<T>();

                _publishers[typeof(T)] = publisher;
            }

            if (publisher is IPublisher<T> typed)
                typed.Publish(toPublish);
            
        }
    }
}
