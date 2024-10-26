
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    public class T23_OnNetworkReady : T23_TriggerBase
    {
        [UdonSynced(UdonSyncMode.None)]
        private bool syncReady;

        private bool synced = false;
        private int firstSyncRequests = 0;

        protected override void PostStart()
        {
            if (Networking.IsOwner(gameObject))
            {
                syncReady = true;
                Trigger();
            }
        }

        public override void OnDeserialization()
        {
            if (!synced && syncReady)
            {
                Trigger();
                synced = true;
                this.enabled = false;
            }
        }
    }
}
