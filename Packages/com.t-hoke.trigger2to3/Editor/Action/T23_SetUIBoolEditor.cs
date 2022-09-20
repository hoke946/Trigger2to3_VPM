﻿#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUIBool))]
    internal class T23_SetUIBoolEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawToggleOperationField();
        }
    }
}
#endif
