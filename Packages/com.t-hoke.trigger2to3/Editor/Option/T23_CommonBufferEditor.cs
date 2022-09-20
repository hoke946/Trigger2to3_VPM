#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using UdonSharpEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_CommonBuffer))]
    internal class T23_CommonBufferEditor : Editor
    {
        T23_CommonBuffer body;

        SerializedProperty prop;

        void OnEnable()
        {
            body = target as T23_CommonBuffer;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            serializedObject.Update();

            T23_EditorUtility.ShowTitle("Option");
            GUILayout.Box("CommonBuffer", T23_EditorUtility.HeadlineStyle());

            UdonSharpGUI.DrawCompileErrorTextArea();

            if (GUILayout.Button("Set Broadcasts"))
            {
                body.broadcasts = T23_EditorUtility.TakeCommonBuffersRelate(body);
            }
            if (GUILayout.Button("Join Empty Broadcasts"))
            {
                T23_EditorUtility.JoinAllBufferingBroadcasts(body);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("autoJoin"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("broadcasts"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
