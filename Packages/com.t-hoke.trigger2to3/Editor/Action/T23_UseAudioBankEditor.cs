
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_UseAudioBank))]
    internal class T23_UseAudioBankEditor : T23_ActionEditorBase
    {
        public enum Command
        {
            Play = 0,
            Stop = 1,
            PlayNext = 2,
            Shuffle = 3
        }

        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("audioBank"));
            var command_prop = serializedObject.FindProperty("command");
            command_prop.intValue = (int)(Command)EditorGUILayout.EnumPopup("Command", (Command)command_prop.intValue);
            if (command_prop.intValue == 0)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("index"));
            }
        }
    }
}
