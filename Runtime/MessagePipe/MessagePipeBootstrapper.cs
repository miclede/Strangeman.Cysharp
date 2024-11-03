using UnityEngine;

namespace Strangeman.Message
{
    public static class MessagePipeBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BootstrapMessagePipe()
        {
            var messageProvider = MessagePipeProvider.Asset;

            if (messageProvider is null)
                Debug.LogError("MessagePipeBootstrapper: No MessagePipeProvider asset located");

            messageProvider.Initialize();

            Debug.Log("MessagePipeBootstrapper.BootstrapMessagePipe: Initialized Provider.");
        }
    }
}
