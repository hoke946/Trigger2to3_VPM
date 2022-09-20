#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetJumpImpulse))]
    internal class T23_SetJumpImpulseEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            PropertyBoxField("impulse", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
