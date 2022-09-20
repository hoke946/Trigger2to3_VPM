#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationTrigger))]
    internal class T23_AnimationTriggerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("trigger"));
        }
    }
}
#endif
