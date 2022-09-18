﻿
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUIFloat))]
    internal class T23_SetUIFloatEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("operation", "propertyBox", "usePropertyBox");
        }
    }
}
