
using UdonSharp;
using UnityEngine;
using VRC.SDK3.UdonNetworkCalling;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    public class T23_BroadcastGlobal : T23_BroadcastBase
    {
        public override bool isGlobal { get { return true; } }

        public NetworkEventTarget sendTarget;

        [Tooltip("0:Always\n1:Master\n2:Owner")]
        public int useablePlayer;

        [Tooltip("0:Unbuffered\n1:BufferOne\n2:Everytime")]
        public int bufferType;

        public T23_CommonBuffer commonBuffer;
        public bool commonBufferSearched;

        private bool synced = false;
        private bool synced2 = false;
        private int actionCount = 0;
        private int missing_count = 0;

        protected override void OnStart()
        {
            if (commonBuffer)
            {
                commonBuffer.LinkBroadcast(this);
            }
        }

        protected override void Trigger_internal()
        {
            if (useablePlayer == 1 && !Networking.IsMaster) { return; }
            if (useablePlayer == 2 && !Networking.IsOwner(gameObject)) { return; }

            if (delayInSeconds > 0)
            {
                SendCustomEventDelayedSeconds(nameof(SendNetworkFire), delayInSeconds);
            }
            else
            {
                SendNetworkFire();
            }
        }

        void Update()
        {
            if (synced) { synced2 = true; }
        }

        public void SetSynced()
        {
            synced = true;
        }

        public void SendNetworkFire()
        {
            if (actions == null)
            {
                return;
            }

            SendCustomNetworkEvent(NetworkEventTarget.Owner, "OwnerProcess", groupID);
            SendCustomNetworkEvent(sendTarget, "RecieveNetworkFire", groupID);

            return;
        }

        public override void Fire()
        {
            if (commonBuffer && !synced2) { return; }   // 初期同期直後は待ちタスクが流れてくる場合があるので１フレーム待つ
            UnconditionalFire();
        }

        public bool UnconditionalFire()
        {
            if (actions == null)
            {
                missing_count++;
                if (missing_count > 3)
                {
                    actions = new UdonSharpBehaviour[0];
                }
                return false;
            }

            actionCount++;
            if (randomize && randomTotal > 0)
            {
                Random.InitState(GetSeed() + actionCount);
                randomValue = Random.Range(0, Mathf.Max(1, randomTotal));
            }

            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].SendCustomEvent("Action");
            }
            return true;
        }

        [NetworkCallable]
        public void RecieveNetworkFire(int gid)
        {
            if (groupID != gid) { return; }
            if (Networking.IsOwner(gameObject)) { return; }
            Fire();
        }

        [NetworkCallable]
        public void OwnerProcess(int gid)
        {
            if (groupID != gid) { return; }
            if (commonBuffer)
            {
                Networking.SetOwner(Networking.LocalPlayer, commonBuffer.gameObject);
                commonBuffer.EntryBuffer(this, bufferType);
            }
            Fire();
        }

        public override int GetSeed()
        {
            if (commonBuffer)
            {
                return commonBuffer.GetSeed(this);
            }
            else
            {
                return actionCount;
            }
        }
    }
}
