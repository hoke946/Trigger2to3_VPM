#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UdonSharp;
using VRC.Udon;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UdonSharpEditor;

namespace Trigger2to3
{
    public class T23_ModuleEditorBase : Editor
    {
        protected T23_ModuleBase body_base;
        protected T23_Master master;
        protected ReorderableList recieverReorderableList;

        void OnEnable()
        {
            body_base = target as T23_ModuleBase;

            master = T23_Master.GetMaster(body_base, body_base.groupID, body_base.category, true, body_base.title);
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            if (!GuideJoinMaster(master, body_base, body_base.groupID, body_base.category))
            {
                return;
            }

            serializedObject.Update();

            T23_EditorUtility.ShowTitle(body_base.category);
            UdonSharpGUI.DrawCompileErrorTextArea();

            if (master)
            {
                GUILayout.Box("[#" + body_base.groupID.ToString() + "] " + body_base.title, T23_EditorUtility.HeadlineStyle());
            }
            else
            {
                body_base.groupID = EditorGUILayout.IntField("Group ID", body_base.groupID);
            }

            DrawActionHeadder();
            DrawFields();
            DrawCommonFields();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawActionHeadder() { }

        protected virtual void DrawFields() { }

        protected virtual void DrawCommonFields() { }

        public static bool GuideJoinMaster(T23_Master master, T23_ModuleBase body, int gid, int category)
        {
            if (master == null)
            {
                EditorGUILayout.HelpBox("Master に組み込まれていません。 このままでも動作しますが、次のボタンで Master に組み込むことができます。", MessageType.Info);
                if (GUILayout.Button("Join Master"))
                {
                    EditorApplication.delayCall += () =>
                    {
                        T23_Master.JoinMaster(body, gid, category);
                        UdonSharpEditorUtility.CopyProxyToUdon(body);
                    };
                }
            }
            return true;
        }
    }
}
#endif
