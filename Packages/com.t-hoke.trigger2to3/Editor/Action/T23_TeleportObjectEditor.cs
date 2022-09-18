
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_TeleportObject))]
    internal class T23_TeleportObjectEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            var byValue_prop = serializedObject.FindProperty("byValue");
            EditorGUILayout.PropertyField(byValue_prop);
            if (byValue_prop.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("local"));
                PropertyBoxField("teleportPosition", "positionPropertyBox", "positionUsePropertyBox");
                PropertyBoxField("teleportRotation", "rotationPropertyBox", "rotationUsePropertyBox");
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("teleportLocation"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("removeVelocity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
