#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_TeleportPlayer))]
    internal class T23_TeleportPlayerEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            var byValue_prop = serializedObject.FindProperty("byValue");
            EditorGUILayout.PropertyField(byValue_prop);
            if (byValue_prop.boolValue)
            {
                PropertyBoxField("teleportPosition", "positionPropertyBox", "positionUsePropertyBox");
                PropertyBoxField("teleportRotation", "rotationPropertyBox", "rotationUsePropertyBox");
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("teleportLocation"));
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("teleportOrientation"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpOnRemote"));
        }
    }
}
#endif
