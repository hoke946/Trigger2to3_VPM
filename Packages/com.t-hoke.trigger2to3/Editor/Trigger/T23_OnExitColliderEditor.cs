#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnExitCollider))]
    internal class T23_OnExitColliderEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerIndividuals"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("layers"));
        }
    }
}
#endif
