
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnExitTrigger : T23_TriggerBase
    {
        public bool triggerIndividuals = true;

        public LayerMask layers = 0;

        private Collider enterCollider;
        private VRCPlayerApi enterPlayer;

        public override bool playerTrigger { get { return true; } }

        private void OnTriggerEnter(Collider other)
        {
            if ((layers.value & 1 << other.gameObject.layer) == 0) { return; }

            if (!triggerIndividuals)
            {
                if (enterCollider) { return; }
                if (enterPlayer != null) { return; }
            }

            enterCollider = other;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!enterCollider || enterCollider && enterCollider != other) { return; }

            Trigger();

            enterCollider = null;
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                if ((layers.value & 1 << LayerMask.NameToLayer("PlayerLocal")) == 0) { return; }
            }
            else
            {
                if ((layers.value & 1 << LayerMask.NameToLayer("Player")) == 0) { return; }
            }

            if (!triggerIndividuals)
            {
                if (enterCollider) { return; }
                if (enterPlayer != null) { return; }
            }

            enterPlayer = player;
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (enterPlayer == null || enterPlayer != null && enterPlayer != player) { return; }

            AnyPlayerTrigger(player);

            enterPlayer = null;
        }
    }
}
