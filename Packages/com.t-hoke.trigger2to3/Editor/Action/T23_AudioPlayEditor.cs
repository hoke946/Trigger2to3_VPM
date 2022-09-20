#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AudioPlay))]
    internal class T23_AudioPlayEditor : T23_ActionEditorBase
    {
        public enum AudioPlayToggleOperation
        {
            Play,
            Stop,
            Toggle
        }

        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUI.BeginChangeCheck();
            var operation = (AudioPlayToggleOperation)EditorGUILayout.EnumPopup("Operation", GetOperation());
            if (EditorGUI.EndChangeCheck())
            {
                SelectOperation(operation);
            }
        }

        private void SelectOperation(AudioPlayToggleOperation operation)
        {
            switch (operation)
            {
                case AudioPlayToggleOperation.Play:
                    serializedObject.FindProperty("toggle").boolValue = false;
                    serializedObject.FindProperty("operation").boolValue = true;
                    break;
                case AudioPlayToggleOperation.Stop:
                    serializedObject.FindProperty("toggle").boolValue = false;
                    serializedObject.FindProperty("operation").boolValue = false;
                    break;
                case AudioPlayToggleOperation.Toggle:
                    serializedObject.FindProperty("toggle").boolValue = true;
                    break;
            }
        }

        private AudioPlayToggleOperation GetOperation()
        {
            if (serializedObject.FindProperty("toggle").boolValue)
            {
                return AudioPlayToggleOperation.Toggle;
            }
            else
            {
                if (serializedObject.FindProperty("operation").boolValue)
                {
                    return AudioPlayToggleOperation.Play;
                }
                else
                {
                    return AudioPlayToggleOperation.Stop;
                }
            }
        }
    }
}
#endif
