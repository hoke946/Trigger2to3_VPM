
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AddVelocity))]
    internal class T23_AddVelocityEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("velocity", "propertyBox", "usePropertyBox");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useWorldSpace"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
