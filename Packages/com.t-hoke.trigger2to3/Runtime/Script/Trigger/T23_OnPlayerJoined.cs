
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnPlayerJoined : T23_TriggerBase
    {
        public bool excludeLocal = true;

        public bool excludePostJoinng = true;

        public override bool playerTrigger { get { return true; } }

        private int frameCount = 100;

        void Update()
        {
            if (frameCount > 0)
            {
                frameCount--;
            }
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                frameCount = 5;
                if (excludeLocal) { return; }
            }
            if (excludePostJoinng && frameCount > 0) { return; }

            AnyPlayerTrigger(player);
        }
    }
}
