
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AddForce))]
    internal class T23_AddForceEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("force", "propertyBox", "usePropertyBox");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("useWorldSpace"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
