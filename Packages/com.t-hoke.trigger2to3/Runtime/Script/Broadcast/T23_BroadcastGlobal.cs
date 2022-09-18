
using UdonSharp;
using UnityEngine;
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

        [UdonSynced(UdonSyncMode.None)]
        private bool syncReady;

        [UdonSynced(UdonSyncMode.None)]
        private int bufferTimes;

        private bool synced = false;
        private bool synced2 = false;
        private int actionCount = 0;
        private int buffering_count = 0;
        private int missing_count = 0;

        [HideInInspector]
        [UdonSynced(UdonSyncMode.None)]
        public int seed;

        protected override void OnStart()
        {
            if (commonBuffer)
            {
                commonBuffer.LinkBroadcast(this);
            }
            else
            {
                if (Networking.IsOwner(gameObject))
                {
                    bufferTimes = 0;
                    seed = Random.Range(0, 1000000000);
                    syncReady = true;
                    RequestSerialization();
                }
                else
                {
                    SendCustomNetworkEvent(NetworkEventTarget.Owner, "RequestFirstSync" + groupID.ToString());
                }
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

            if (!synced && syncReady)
            {
                if (!commonBuffer)
                {
                    if (buffering_count < bufferTimes)
                    {
                        if (!UnconditionalFire()) { return; }
                        buffering_count++;
                        return;
                    }
                    SetSynced();
                }
            }
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

            SendCustomNetworkEvent(NetworkEventTarget.Owner, "OwnerProcess" + groupID.ToString());
            SendCustomNetworkEvent(sendTarget, "RecieveNetworkFire" + groupID.ToString());

            return;
        }

        public override void Fire()
        {
            if (!synced2) { return; }   // 初期同期直後は待ちタスクが流れてくる場合があるので１フレーム待つ
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

        /*
        private T23_BroadcastGlobal GetCorrectBroadcast(int id)
        {
            var bgs = GetComponents<T23_BroadcastGlobal>();
            foreach (var bg in bgs)
            {
                if (bg.groupID == id)
                {
                    return bg;
                }
            }
            return null;
        }
        */

        public void RecieveNetworkFire0()
        {
            if (groupID == 0) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire1()
        {
            if (groupID == 1) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire2()
        {
            if (groupID == 2) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire3()
        {
            if (groupID == 3) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire4()
        {
            if (groupID == 4) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire5()
        {
            if (groupID == 5) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire6()
        {
            if (groupID == 6) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire7()
        {
            if (groupID == 7) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire8()
        {
            if (groupID == 8) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire9()
        {
            if (groupID == 9) { RecieveNetworkFire(); }
        }

        public void RecieveNetworkFire()
        {
            if (Networking.IsOwner(gameObject)) { return; }
            Fire();
        }

        public void OwnerProcess0()
        {
            if (groupID == 0) { OwnerProcess(); }
        }

        public void OwnerProcess1()
        {
            if (groupID == 1) { OwnerProcess(); }
        }

        public void OwnerProcess2()
        {
            if (groupID == 2) { OwnerProcess(); }
        }

        public void OwnerProcess3()
        {
            if (groupID == 3) { OwnerProcess(); }
        }

        public void OwnerProcess4()
        {
            if (groupID == 4) { OwnerProcess(); }
        }

        public void OwnerProcess5()
        {
            if (groupID == 5) { OwnerProcess(); }
        }

        public void OwnerProcess6()
        {
            if (groupID == 6) { OwnerProcess(); }
        }

        public void OwnerProcess7()
        {
            if (groupID == 7) { OwnerProcess(); }
        }

        public void OwnerProcess8()
        {
            if (groupID == 8) { OwnerProcess(); }
        }

        public void OwnerProcess9()
        {
            if (groupID == 9) { OwnerProcess(); }
        }

        public void OwnerProcess()
        {
            if (commonBuffer)
            {
                Networking.SetOwner(Networking.LocalPlayer, commonBuffer.gameObject);
                commonBuffer.EntryBuffer(this, bufferType);
            }
            else
            {
                if (bufferType == 1)
                {
                    if (bufferTimes == 0)
                    {
                        bufferTimes = 1;
                        RequestSerialization();
                    }
                }
                else if (bufferType == 2)
                {
                    bufferTimes++;
                    RequestSerialization();
                }
            }
            Fire();
        }

        public bool IsSyncReady()
        {
            return syncReady;
        }

        public override int GetSeed()
        {
            if (commonBuffer)
            {
                return commonBuffer.GetSeed(this);
            }
            else
            {
                return seed;
            }
        }
    }
}
