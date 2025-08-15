
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnPlayerRespawn : T23_TriggerBase
    {
        public bool localOnly;

        public override bool playerTrigger { get { return true; } }

        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (!localOnly || player == Networking.LocalPlayer)
            {
                AnyPlayerTrigger(player);
            }
        }
    }
}
