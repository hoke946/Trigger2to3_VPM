
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnParticleCollision : T23_TriggerBase
    {
        // 用途・処理方法不明
        //[SerializeField]
        //private bool triggerIndividuals = true;

        public LayerMask layers = 0;

        public override bool playerTrigger { get { return true; } }

        private void OnParticleCollision(GameObject other)
        {
            if ((layers.value & 1 << other.layer) == 0) { return; }

            Trigger();
        }

        public override void OnPlayerParticleCollision(VRCPlayerApi player)
        {
            if (player == Networking.LocalPlayer)
            {
                if ((layers.value & 1 << LayerMask.NameToLayer("PlayerLocal")) == 0) { return; }
            }
            else
            {
                if ((layers.value & 1 << LayerMask.NameToLayer("Player")) == 0) { return; }
            }

            AnyPlayerTrigger(player);
        }
    }
}
