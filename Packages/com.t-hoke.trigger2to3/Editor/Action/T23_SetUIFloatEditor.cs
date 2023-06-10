#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUIFloat))]
    internal class T23_SetUIFloatEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("withoutNotify"));
        }
    }
}
#endif
