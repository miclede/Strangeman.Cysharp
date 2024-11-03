using MessagePipe;
using System;
using System.Collections.Generic;

namespace Strangeman.Message
{
    public class GlobalMessageSubscriber
    {
        private readonly List<IDisposable> _disposables = new();

        public void SubscribeTo<T>(Action<T> eventHandler)
        {
            var subscriber = GlobalMessagePipe.GetSubscriber<T>();
            var disposable = subscriber.Subscribe(eventHandler);
            _disposables.Add(disposable);
        }

        //Call on Destroy
        public void Dispose()
        {
            _disposables.ForEach(x => x.Dispose());
        }
    }
}
