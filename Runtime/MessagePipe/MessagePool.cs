using System;
using System.Collections.Generic;

namespace Strangeman.Message
{
    public class MessagePool<M, T> where M : IMessage<T>
    {
        readonly Queue<M> _cachedMessages = new();
        readonly Func<M> _messageFactory;

        public MessagePool(Func<M> messageFactory)
        {
            _messageFactory = messageFactory;
        }

        public void ReturnToPool(M message)
        {
            message.ResetMessage();
            _cachedMessages.Enqueue(message);
        }

        public M GetMessageFromPool(T content)
        {
            M message;

            if (_cachedMessages.Count > 0)
            {
                message = _cachedMessages.Dequeue();
            }
            else
            {
                message = _messageFactory();
            }

            message.SetMessage(content);
            return message;
        }
    }
}
