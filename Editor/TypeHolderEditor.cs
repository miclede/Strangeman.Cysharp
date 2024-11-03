#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Strangeman.Message.Editor
{
    [CustomEditor(typeof(MessagePipeProvider))]
    public class TypeHolderEditor : UnityEditor.Editor
    {
        private string[] availableTypes;

        private void OnEnable()
        {
            // Find all types that implement any generic version of IMessage<>
            availableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces()
                             .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessage<>))
                       && !t.IsAbstract) // Ensure it's a concrete implementation
                .Select(t => t.FullName)
                .ToArray();
        }

        public override void OnInspectorGUI()
        {
            var typeHolder = (MessagePipeProvider)target;

            // Initialize MessageTypes if null
            if (typeHolder.MessageTypes == null)
                typeHolder.MessageTypes = new List<string>();

            // Ensure uniqueness in MessageTypes
            List<string> messageTypesList = typeHolder.MessageTypes.Distinct().ToList();

            if (GUILayout.Button("Add All"))
            {
                foreach (var t in availableTypes)
                {
                    // Only add unique types
                    if (!typeHolder.MessageTypes.Contains(t))
                    {
                        typeHolder.MessageTypes.Add(t);
                    }
                }
                SaveMessagePipe();
            }

            // Button to add a new type
            if (GUILayout.Button("Add Message"))
            {
                foreach (var type in availableTypes)
                {
                    // Only add if it doesn't already exist
                    if (!typeHolder.MessageTypes.Contains(type))
                    {
                        typeHolder.MessageTypes.Add(type);
                        break; // Exit after adding one type
                    }
                }
                SaveMessagePipe();
            }

            // Display each type as a dropdown
            for (int i = 0; i < messageTypesList.Count; i++)
            {
                // Get the current type
                string currentType = messageTypesList[i];
                int selectedIndex = Array.IndexOf(availableTypes, currentType);
                selectedIndex = EditorGUILayout.Popup(selectedIndex, availableTypes);

                // Check if the user selected a valid option and if it differs from the current type
                if (selectedIndex >= 0 && availableTypes[selectedIndex] != currentType)
                {
                    // Remove the current type from the list
                    typeHolder.MessageTypes.Remove(currentType);

                    // Add the newly selected type if it's not already in the list
                    if (!typeHolder.MessageTypes.Contains(availableTypes[selectedIndex]))
                    {
                        typeHolder.MessageTypes.Add(availableTypes[selectedIndex]);
                    }

                    SaveMessagePipe();
                }
            }

            // Optional: Display all current types with remove buttons
            for (int i = 0; i < messageTypesList.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(messageTypesList[i]);

                if (GUILayout.Button("Remove"))
                {
                    typeHolder.MessageTypes.Remove(messageTypesList[i]);
                    SaveMessagePipe();
                    break; // Break to avoid modifying collection during iteration
                }
                EditorGUILayout.EndHorizontal();
            }


            // Button to remove all types
            if (GUILayout.Button("Remove All"))
            {
                typeHolder.MessageTypes.Clear(); // Clear all entries
                SaveMessagePipe();
            }
        }

        private static void SaveMessagePipe()
        {
            EditorUtility.SetDirty(MessagePipeProvider.Asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif
