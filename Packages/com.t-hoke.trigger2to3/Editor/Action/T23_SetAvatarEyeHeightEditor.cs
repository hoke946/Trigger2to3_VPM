#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetAvatarEyeHeight))]
    internal class T23_SetAvatarEyeHeightEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            PropertyBoxField("scaleMeters", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
