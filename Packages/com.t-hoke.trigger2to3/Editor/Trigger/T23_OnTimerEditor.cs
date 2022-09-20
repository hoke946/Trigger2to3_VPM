#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnTimer))]
    internal class T23_OnTimerEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("repeat"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("resetOnEnable"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lowPeriodTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("highPeriodTime"));
        }
    }
}
#endif
