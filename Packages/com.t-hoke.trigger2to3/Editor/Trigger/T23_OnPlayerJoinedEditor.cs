#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPlayerJoined))]
    internal class T23_OnPlayerJoinedEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("excludeLocal"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("excludePostJoinng"));
        }
    }
}
#endif
