#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnKeyUp))]
    internal class T23_OnKeyUpEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("key"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("keyFree"));
        }
    }
}
#endif
