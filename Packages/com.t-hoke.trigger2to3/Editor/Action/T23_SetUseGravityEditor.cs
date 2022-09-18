
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUseGravity))]
    internal class T23_SetUseGravityEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawBoolOperationField();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("takeOwnership"));
        }
    }
}
