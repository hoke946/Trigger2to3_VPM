#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetUIText))]
    internal class T23_SetUITextEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            DrawRecieversList();
            PropertyBoxField("text", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
