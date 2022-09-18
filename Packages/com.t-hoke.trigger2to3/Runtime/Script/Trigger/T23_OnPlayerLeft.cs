
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnPlayerLeft : T23_TriggerBase
    {
        public bool excludeLocal;

        public override bool playerTrigger { get { return true; } }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (excludeLocal && player == Networking.LocalPlayer) { return; }

            AnyPlayerTrigger(player);
        }
    }
}
