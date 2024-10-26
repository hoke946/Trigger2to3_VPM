
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnAvatarEyeHeightChanged : T23_TriggerBase
    {
        public bool localOnly = true;

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                Trigger();
            }
        }
    }
}
