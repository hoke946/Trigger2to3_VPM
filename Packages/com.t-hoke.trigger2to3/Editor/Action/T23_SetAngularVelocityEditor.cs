
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetAngularVelocity))]
    internal class T23_SetAngularVelocityEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("angularVelocity", "propertyBox", "usePropertyBox");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useWorldSpace"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
