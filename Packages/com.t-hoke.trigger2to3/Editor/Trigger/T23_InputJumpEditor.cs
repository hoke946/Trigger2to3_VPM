
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_InputJump))]
    internal class T23_InputJumpEditor : T23_TriggerEditorBase
    {
        enum InputValue
        {
            Down = 1,
            Up = 0
        }

        protected override void DrawFields()
        {
            serializedObject.FindProperty("inputValue").boolValue = (InputValue)EditorGUILayout.EnumPopup("Value", (InputValue)System.Convert.ToInt32(serializedObject.FindProperty("inputValue").boolValue)) == InputValue.Down;
        }
    }
}
