
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetIsKinematic))]
    internal class T23_SetIsKinematicEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawBoolOperationField();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
