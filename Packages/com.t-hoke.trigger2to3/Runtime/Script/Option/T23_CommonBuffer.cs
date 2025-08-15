
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    [AddComponentMenu("Trigger2to3/Trigger2to3 CommonBuffer (T23_CommonBuffer)")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class T23_CommonBuffer : UdonSharpBehaviour
    {
        public T23_BroadcastGlobal[] broadcasts;

        public bool autoJoin = true;

        [UdonSynced(UdonSyncMode.None)]
        private bool syncReady;

        [UdonSynced(UdonSyncMode.None)]
        private int[] broadcastIdx = new int[0];

        private bool synced = false;
        private int buffering_count = 0;
        private int[] onHoldBuffer = new int[0];

        [UdonSynced(UdonSyncMode.None)]
        private int seed;

        void Start()
        {
            if (Networking.IsOwner(gameObject))
            {
                seed = Random.Range(0, 1000000000);
                syncReady = true;
                RequestSerialization();
            }
        }

        void Update()
        {
            for (int i = 0; i < onHoldBuffer.Length; i++)
            {
                if (broadcasts[onHoldBuffer[i]].gameObject.activeInHierarchy)
                {
                    if (!broadcasts[onHoldBuffer[i]].UnconditionalFire()) { break; }
                    onHoldBuffer = RemoveIntArray(onHoldBuffer, i);
                    break;
                }
            }

            if (!synced && syncReady)
            {
                if (buffering_count < broadcastIdx.Length)
                {
                    var broadcast = broadcasts[broadcastIdx[buffering_count]];
                    if (!broadcast.gameObject.activeInHierarchy)
                    {
                        DeepActivate(broadcast.gameObject, broadcast.gameObject);
                        if (!broadcast.gameObject.activeInHierarchy)
                        {
                            onHoldBuffer = AddIntArray(onHoldBuffer, broadcastIdx[buffering_count]);
                            buffering_count++;
                        }
                        return;
                    }
                    if (!broadcast.UnconditionalFire()) { return; }
                    buffering_count++;

                    return;
                }
                foreach (var broadcast in broadcasts)
                {
                    broadcast.SetSynced();
                }
                synced = true;
            }
        }

        private void DeepActivate(GameObject current, GameObject target)
        {
            current.SetActive(current);
            if (!target.activeInHierarchy)
            {
                if (current.transform.parent)
                {
                    DeepActivate(current.transform.parent.gameObject, target);
                }
            }
        }

        public void LinkBroadcast(T23_BroadcastGlobal broadcast)
        {
            if (broadcasts == null)
            {
                broadcasts = new T23_BroadcastGlobal[1];
                broadcasts[0] = broadcast;
            }
            else
            {
                bool contains = false;
                for (int i = 0; i < broadcasts.Length; i++)
                {
                    if (broadcasts[i] == broadcast)
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    broadcasts = AddBroadcastGlobalArray(broadcasts, broadcast);
                }
            }
            if (synced)
            {
                broadcast.SetSynced();
            }
        }

        public void EntryBuffer(T23_BroadcastGlobal broadcast, int bufferType)
        {
            if (bufferType == 0) { return; }

            for (int bidx = 0; bidx < broadcasts.Length; bidx++)
            {
                if (broadcast == broadcasts[bidx])
                {
                    if (bufferType == 1)
                    {
                        int exist = FindValueIntArray(broadcastIdx, bidx, 0);
                        if (exist != -1)
                        {
                            broadcastIdx = RemoveIntArray(broadcastIdx, exist);
                        }
                    }

                    broadcastIdx = AddIntArray(broadcastIdx, bidx);
                }
            }
            RequestSerialization();
        }

        private int[] AddIntArray(int[] array, int value)
        {
            int[] new_array = new int[array.Length + 1];
            array.CopyTo(new_array, 0);
            new_array[new_array.Length - 1] = value;
            return new_array;
        }

        public int FindValueIntArray(int[] array, int value, int start)
        {
            for (int i = start; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        private int[] RemoveIntArray(int[] array, int index)
        {
            int[] new_array = new int[array.Length - 1];
            for (int i = 0; i < index; i++)
            {
                new_array[i] = array[i];
            }
            for (int i = index; i < new_array.Length; i++)
            {
                new_array[i] = array[i + 1];
            }
            return new_array;
        }

        private T23_BroadcastGlobal[] AddBroadcastGlobalArray(T23_BroadcastGlobal[] array, T23_BroadcastGlobal value)
        {
            T23_BroadcastGlobal[] new_array = new T23_BroadcastGlobal[array.Length + 1];
            array.CopyTo(new_array, 0);
            new_array[new_array.Length - 1] = value;
            return new_array;
        }

        public int GetSeed(T23_BroadcastGlobal broadcast)
        {
            for (int i = 0; i < broadcasts.Length; i++)
            {
                if (broadcasts[i] == broadcast)
                {
                    return seed + i;
                }
            }
            return seed;
        }
    }
}
