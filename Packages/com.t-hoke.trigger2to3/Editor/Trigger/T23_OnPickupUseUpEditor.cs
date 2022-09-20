#if UNITY_EDITOR && !COMPILER_UDONSHARP
using VRC.SDKBase;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPickupUseUp))]
    internal class T23_OnPickupUseUpEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            var pickup = body_base.GetComponent<VRC_Pickup>();
            if (pickup)
            {
                if (pickup.AutoHold != VRC_Pickup.AutoHoldMode.Yes)
                {
                    EditorGUILayout.HelpBox("VRC_Pickup の AutoHold を Yes にする必要があります。", MessageType.Warning);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("VRC_Pickup が必要です。", MessageType.Warning);
            }
        }
    }
}
#endif
