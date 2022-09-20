#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_CallUdonMethod))]
    internal class T23_CallUdonMethodEditor : T23_ActionEditorBase
    {
        public enum OwnershipControl
        {
            None = 0,
            SendOwner = 1,
            TakeOwnership = 2
        }

        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("udonBehaviour"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("method"));
            var ownershipControl_prop = serializedObject.FindProperty("ownershipControl");
            ownershipControl_prop.intValue = System.Convert.ToInt32(EditorGUILayout.EnumPopup("Ownership Control", (OwnershipControl)ownershipControl_prop.intValue));
        }
    }
}
#endif
