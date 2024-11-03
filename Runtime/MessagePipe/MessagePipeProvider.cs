using UnityEngine;
using MessagePipe;
using System;
using Strangeman.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Strangeman.Message
{
    [CreateAssetMenu(fileName = k_messagePipeProviderName, menuName = "Strangeman/Messaging/PipeProvider")]
    public class MessagePipeProvider : ScriptableObject, IInitializable
    {
        public List<string> MessageTypes;

        private IServiceProvider _provider;

        #region Static Asset Ref for Bootstrapping
        public const string k_messagePipeProviderName = "MessagePipeProvider";
        private static MessagePipeProvider _asset;
        public static MessagePipeProvider Asset
        {
            get
            {
                if (_asset is null)
                {
                    var assetAtPath = Resources.Load<MessagePipeProvider>(k_messagePipeProviderName);

                    _asset = assetAtPath != null ? assetAtPath : throw new NullReferenceException($"{k_messagePipeProviderName} does not exist in a resource folder, create via asset menu.");
                }
                return _asset;
            }
        }
        #endregion

        public void Initialize()
        {
            var builder = new BuiltinContainerBuilder();

            builder.AddMessagePipe();

            AddBrokers(builder);

            _provider = builder.BuildServiceProvider();
            GlobalMessagePipe.SetProvider(_provider);
        }

        private void AddBrokers(BuiltinContainerBuilder b)
        {
            var messagetypes = new List<Type>();

            foreach (var typeName in MessageTypes)
            {
                Type type = Type.GetType(typeName);

                if (type == null)
                {
                    type = AppDomain.CurrentDomain.GetAssemblies()
                        .Select(assembly => assembly.GetType(typeName))
                        .FirstOrDefault(t => t != null);
                }

                if (type != null)
                    messagetypes.Add(type);
                else Debug.LogWarning($"Type: '{typeName}' not found in assemblies.");
            }

            foreach (var message in messagetypes)
            {
                // Get the method with one generic parameter
                var method = typeof(BuiltinContainerBuilder)
                    .GetMethods()
                    .FirstOrDefault(m => m.Name == "AddMessageBroker" && m.GetGenericArguments().Length == 1);

                if (method != null)
                {
                    var generic = method.MakeGenericMethod(message);

                    generic.Invoke(b, null);
                }
            }
        }
    }
}
