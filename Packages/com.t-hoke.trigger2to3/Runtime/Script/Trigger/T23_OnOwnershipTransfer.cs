
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnOwnershipTransfer : T23_TriggerBase
    {
        public bool localOnly = false;

        public override bool playerTrigger { get { return true; } }

        public override void OnOwnershipTransferred(VRCPlayerApi player)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                AnyPlayerTrigger(player);
            }
        }
    }
}
