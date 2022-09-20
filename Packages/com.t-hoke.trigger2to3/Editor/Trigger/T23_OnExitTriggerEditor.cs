#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnExitTrigger))]
    internal class T23_OnExitTriggerEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerIndividuals"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("layers"));
        }
    }
}
#endif
