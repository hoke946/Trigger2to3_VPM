#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetParent))]
    internal class T23_SetParentEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("parent"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("worldPositionStays"));
        }
    }
}
#endif
