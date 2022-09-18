
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_BroadcastLocal : T23_BroadcastBase
    {
        public override bool isGlobal { get { return false; } }

        protected override void Trigger_internal()
        {
            if (delayInSeconds > 0)
            {
                SendCustomEventDelayedSeconds(nameof(Fire), delayInSeconds);
            }
            else
            {
                Fire();
            }
        }

        public override void Fire()
        {
            if (actions == null) { return; }

            if (randomize && randomTotal > 0)
            {
                randomValue = Random.Range(0, Mathf.Max(1, randomTotal));
            }

            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].SendCustomEvent("Action");
            }
        }
    }
}
