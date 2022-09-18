
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationBool))]
    internal class T23_AnimationBoolEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("variable"));
            DrawToggleOperationField();
        }
    }
}
