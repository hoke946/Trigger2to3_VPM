#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_BroadcastGlobal))]
    internal class T23_BroadcastGlobalEditor : T23_BroadcastEditorBase
    {
        T23_BroadcastGlobal body;

        public enum UsablePlayer
        {
            Always = 0,
            Master = 1,
            Owner = 2
        }

        public enum BufferType
        {
            Unbuffered = 0,
            BufferOne = 1,
            Everytime = 2
        }

        protected override void DrawFields()
        {
            body = target as T23_BroadcastGlobal;

            if (master)
            {
                master.randomize = body.randomize;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sendTarget"));
            serializedObject.FindProperty("useablePlayer").intValue = (int)(UsablePlayer)EditorGUILayout.EnumPopup("Usable Player", (UsablePlayer)body.useablePlayer);
            serializedObject.FindProperty("bufferType").intValue = (int)(BufferType)EditorGUILayout.EnumPopup("Buffer Type", (BufferType)body.bufferType);

            if (body.bufferType == (int)BufferType.Unbuffered)
            {
                if (body.commonBuffer != null)
                {
                    body.commonBuffer = null;
                    T23_EditorUtility.UpdateAllCommonBuffersRelate();
                }
                body.commonBufferSearched = false;
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("commonBuffer"));
                if (body.commonBuffer == null)
                {
                    var buttonStyle = new GUIStyle(GUI.skin.button);
                    buttonStyle.fontSize = 10;
                    buttonStyle.stretchWidth = false;
                    if (GUILayout.Button("Add CommonBuffer", buttonStyle))
                    {
                        var added = T23_EditorUtility.AddCommonBuffer();
                        T23_EditorUtility.JoinAllBufferingBroadcasts(added);
                    }
                }
                EditorGUILayout.EndHorizontal();
                if (EditorGUI.EndChangeCheck())
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    T23_EditorUtility.UpdateAllCommonBuffersRelate();
                }
                if (body.commonBuffer == null)
                {
                    if (body.commonBufferSearched)
                    {
                        EditorGUILayout.HelpBox(T23_Localization.GetWord("Broadcast_commonbuffer"), MessageType.Warning);
                    }
                    else
                    {
                        body.commonBuffer = T23_EditorUtility.GetAutoJoinCommonBuffer(body);
                        if (body.commonBuffer != null)
                        {
                            T23_EditorUtility.UpdateAllCommonBuffersRelate();
                        }
                        body.commonBufferSearched = true;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDestroy()
        {
            T23_EditorUtility.UpdateAllCommonBuffersRelate();
        }
    }
}
#endif
