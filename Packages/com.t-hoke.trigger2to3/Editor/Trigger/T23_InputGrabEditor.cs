#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_InputGrab))]
    internal class T23_InputGrabEditor : T23_TriggerEditorBase
    {
        enum InputValue
        {
            Down = 1,
            Up = 0
        }

        enum HandType
        {
            Any = 0,
            Right = 1,
            Left = 2
        }

        protected override void DrawFields()
        {
            serializedObject.FindProperty("inputValue").boolValue = (InputValue)EditorGUILayout.EnumPopup("Value", (InputValue)System.Convert.ToInt32(serializedObject.FindProperty("inputValue").boolValue)) == InputValue.Down;
            serializedObject.FindProperty("hand").intValue = (int)(HandType)EditorGUILayout.EnumPopup("Hand", (HandType)System.Convert.ToInt32(serializedObject.FindProperty("hand").intValue));
        }
    }
}
#endif
