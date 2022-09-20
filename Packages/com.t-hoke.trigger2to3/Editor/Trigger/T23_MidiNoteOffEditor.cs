#if UNITY_EDITOR && !COMPILER_UDONSHARP
using VRC.SDKBase;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_MidiNoteOff))]
    internal class T23_MidiNoteOffEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            serializedObject.FindProperty("channel").intValue = System.Convert.ToInt32(EditorGUILayout.EnumPopup("Channel", (VRC_MidiNoteIn.Channel)serializedObject.FindProperty("channel").intValue));
            serializedObject.FindProperty("note").intValue = System.Convert.ToInt32(EditorGUILayout.EnumPopup("Note", (VRC_MidiNoteIn.Note)serializedObject.FindProperty("note").intValue));
        }
    }
}
#endif
