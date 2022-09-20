#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AddAngularVelocity))]
    internal class T23_AddAngularVelocityEditor : T23_ActionEditorBase
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
#endif
