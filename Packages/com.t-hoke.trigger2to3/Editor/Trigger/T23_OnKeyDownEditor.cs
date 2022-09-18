
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnKeyDown))]
    internal class T23_OnKeyDownEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("key"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("keyFree"));
        }
    }
}
