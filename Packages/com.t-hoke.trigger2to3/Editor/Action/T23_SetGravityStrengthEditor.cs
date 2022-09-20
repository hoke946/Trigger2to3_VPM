#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetGravityStrength))]
    internal class T23_SetGravityStrengthEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            PropertyBoxField("gravityStrength", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
