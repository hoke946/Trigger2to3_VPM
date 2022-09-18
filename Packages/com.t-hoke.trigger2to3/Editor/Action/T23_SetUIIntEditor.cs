﻿
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUIInt))]
    internal class T23_SetUIIntEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
        }
    }
}
