#if UNITY_EDITOR && !COMPILER_UDONSHARP
using VRC.SDKBase;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_MidiControlChange))]
    internal class T23_MidiControlChangeEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            serializedObject.FindProperty("channel").intValue = System.Convert.ToInt32(EditorGUILayout.EnumPopup("Channel", (VRC_MidiNoteIn.Channel)serializedObject.FindProperty("channel").intValue));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("number"));
        }
    }
}
#endif
