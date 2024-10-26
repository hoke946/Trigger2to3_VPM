
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnAvatarChanged : T23_TriggerBase
    {
        public bool localOnly = true;

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                Trigger();
            }
        }
    }
}
