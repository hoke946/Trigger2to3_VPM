#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AudioPause))]
    internal class T23_AudioPauseEditor : T23_ActionEditorBase
    {
        public enum PauseOperation
        {
            Pause = 1,
            Unpause = 0
        }

        protected override void DrawFields()
        {
            DrawRecieversList();
            var operation_prop = serializedObject.FindProperty("operation");
            operation_prop.boolValue = (PauseOperation)EditorGUILayout.EnumPopup("Operation", (PauseOperation)System.Convert.ToInt32(operation_prop.boolValue)) == PauseOperation.Pause;
        }
    }
}
#endif
