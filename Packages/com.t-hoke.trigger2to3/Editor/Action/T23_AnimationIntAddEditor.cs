#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationIntAdd))]
    internal class T23_AnimationIntAddEditor : T23_ActionEditorBase
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
