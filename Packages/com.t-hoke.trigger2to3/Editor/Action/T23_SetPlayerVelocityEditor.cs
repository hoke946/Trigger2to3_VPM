#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetPlayerVelocity))]
    internal class T23_SetPlayerVelocityEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            PropertyBoxField("velocity", "propertyBox", "usePropertyBox");
        }
    }
}
#endif
