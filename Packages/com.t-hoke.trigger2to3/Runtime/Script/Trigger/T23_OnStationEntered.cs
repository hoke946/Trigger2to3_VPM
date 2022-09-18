
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnStationEntered : T23_TriggerBase
    {
        public bool localOnly = false;

        public override void OnStationEntered(VRCPlayerApi player)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                Trigger();
            }
        }
    }
}
