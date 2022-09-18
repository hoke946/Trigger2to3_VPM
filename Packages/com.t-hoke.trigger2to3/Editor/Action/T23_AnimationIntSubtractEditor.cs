
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationIntSubtract))]
    internal class T23_AnimationIntSubtractEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("variable"));
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
        }
    }
}
