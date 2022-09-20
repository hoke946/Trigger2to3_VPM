#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnEnterCollider))]
    internal class T23_OnEnterColliderEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerIndividuals"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("layers"));
        }
    }
}
#endif
