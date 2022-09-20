#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationInt))]
    internal class T23_AnimationIntEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("variable"));
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
