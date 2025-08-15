
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_TriggerBase : T23_ModuleBase
    {
        public override int category { get { return 1; } }

        protected T23_BroadcastBase broadcast;

        [HideInInspector]
        public VRCPlayerApi triggeredPlayer = Networking.LocalPlayer;
        public virtual bool playerTrigger { get { return false; } }

        protected override void OnStart()
        {
            T23_BroadcastBase[] broadcasts = GetComponents<T23_BroadcastBase>();
            for (int i = 0; i < broadcasts.Length; i++)
            {
                if (broadcasts[i].groupID == groupID)
                {
                    broadcast = broadcasts[i];
                    break;
                }
            }
        }

        public void Trigger()
        {
            if (!gameObject.activeInHierarchy) { return; }
            if (broadcast)
            {
                broadcast.Trigger();
            }
        }

        public void AnyPlayerTrigger(VRCPlayerApi player)
        {
            triggeredPlayer = player;
            if (broadcast)
            {
                broadcast.AnyPlayerTrigger(player);
            }
        }
    }
}
