#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_CustomTrigger))]
    internal class T23_CustomTriggerEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"));
        }
    }
}
#endif
