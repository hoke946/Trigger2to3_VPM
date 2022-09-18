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
    public class T23_BroadcastEditorBase : T23_ModuleEditorBase
    {
        protected override void DrawCommonFields()
        {
            body_base = target as T23_ModuleBase;
            if (master)
            {
                master.randomize = serializedObject.FindProperty("randomize").boolValue;
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("delayInSeconds"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("randomize"));
        }
    }
}
#endif
