﻿#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetChildrenActive))]
    internal class T23_SetChildrenActiveEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            DrawBoolOperationField();
        }
    }
}
#endif
