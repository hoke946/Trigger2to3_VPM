#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetPlayerSpeed))]
    internal class T23_SetPlayerSpeedEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            PropertyBoxField("walkSpeed", "propertyBox_walk", "usePropertyBox_walk");
            PropertyBoxField("runSpeed", "propertyBox_run", "usePropertyBox_run");
            PropertyBoxField("strafeSpeed", "propertyBox_strafe", "usePropertyBox_strafe");
        }
    }
}
#endif
