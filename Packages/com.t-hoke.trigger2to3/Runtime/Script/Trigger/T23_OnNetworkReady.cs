
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

        void Update()
        {
            if (!synced && syncReady)
            {
                Trigger();
                synced = true;
                this.enabled = false;
            }

            ActivitySwitching();
        }

        private void ActivitySwitching()
        {
            if (!synced || firstSyncRequests > 0)
            {
                this.enabled = true;
            }
            else
            {
                this.enabled = false;
            }
        }
    }
}
