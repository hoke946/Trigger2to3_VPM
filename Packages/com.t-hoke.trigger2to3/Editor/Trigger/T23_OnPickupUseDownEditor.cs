#if UNITY_EDITOR && !COMPILER_UDONSHARP
using VRC.SDKBase;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPickupUseDown))]
    internal class T23_OnPickupUseDownEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            var pickup = body_base.GetComponent<VRC_Pickup>();
            if (pickup)
            {
                if (pickup.AutoHold != VRC_Pickup.AutoHoldMode.Yes)
                {
                    EditorGUILayout.HelpBox(T23_Localization.GetWord("OnPickup_autohold"), MessageType.Warning);
                }
            }
            else
            {
                EditorGUILayout.HelpBox(T23_Localization.GetWord("OnPickup_required"), MessageType.Warning);
            }
        }
    }
}
#endif
