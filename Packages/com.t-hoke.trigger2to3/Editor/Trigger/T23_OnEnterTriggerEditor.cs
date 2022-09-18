﻿
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnEnterTrigger))]
    internal class T23_OnEnterTriggerEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerIndividuals"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("layers"));
        }
    }
}
