
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnExitCollider : T23_TriggerBase
    {
        public bool triggerIndividuals = true;

        public LayerMask layers = 0;

        private Collision enterCollider;
        private VRCPlayerApi enterPlayer;

        public override bool playerTrigger { get { return true; } }

        private void OnCollisionEnter(Collision other)
        {
            if ((layers.value & 1 << other.gameObject.layer) == 0) { return; }

            if (!triggerIndividuals)
            {
                if (enterCollider != null) { return; }
                if (enterPlayer != null) { return; }
            }

            enterCollider = other;
        }

        private void OnCollisionExit(Collision other)
        {
            if (enterCollider == null || enterCollider != null && enterCollider != other) { return; }

            Trigger();

            enterCollider = null;
        }

        public override void OnPlayerCollisionEnter(VRCPlayerApi player)
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
                if (enterCollider != null) { return; }
                if (enterPlayer != null) { return; }
            }

            enterPlayer = player;
        }

        public override void OnPlayerCollisionExit(VRCPlayerApi player)
        {
            if (enterPlayer == null || enterPlayer != null && enterPlayer != player) { return; }

            AnyPlayerTrigger(player);

            enterPlayer = null;
        }
    }
}
