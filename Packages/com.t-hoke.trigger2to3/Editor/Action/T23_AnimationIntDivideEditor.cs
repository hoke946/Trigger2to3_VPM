﻿
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_AnimationIntDivide))]
    internal class T23_AnimationIntDivideEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("variable"));
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
        }
    }
}
