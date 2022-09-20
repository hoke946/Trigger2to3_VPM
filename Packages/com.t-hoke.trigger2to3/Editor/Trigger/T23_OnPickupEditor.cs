#if UNITY_EDITOR && !COMPILER_UDONSHARP
using VRC.SDKBase;
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPickup))]
    internal class T23_OnPickupEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            var pickup = body_base.GetComponent<VRC_Pickup>();
            if (!pickup)
            {
                EditorGUILayout.HelpBox("VRC_Pickup が必要です。", MessageType.Warning);
            }
        }
    }
}
#endif
