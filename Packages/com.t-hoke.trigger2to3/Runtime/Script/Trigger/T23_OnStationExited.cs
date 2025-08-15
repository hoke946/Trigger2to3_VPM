
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnStationExited : T23_TriggerBase
    {
        public bool localOnly = false;

        public override void OnStationExited(VRCPlayerApi player)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                Trigger();
            }
        }
    }
}
