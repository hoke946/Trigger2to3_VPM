
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace Trigger2to3
{
    public class T23_BroadcastBase : T23_ModuleBase
    {
        public override int category { get { return 0; } }

        public virtual bool isGlobal { get { return false; } }

        public float delayInSeconds;

        public bool randomize;

        [HideInInspector]
        public float randomTotal;

        [HideInInspector]
        public float randomValue = 0;

        protected UdonSharpBehaviour[] actions;
        protected int[] priorities;

        [HideInInspector]
        public VRCPlayerApi triggeredPlayer;

        public virtual void Trigger()
        {
            triggeredPlayer = Networking.LocalPlayer;
            Trigger_internal();
        }

        public virtual void AnyPlayerTrigger(VRCPlayerApi player)
        {
            triggeredPlayer = player;
            Trigger_internal();
        }

        protected virtual void Trigger_internal() { }

        public virtual void Fire() { }

        public void AddActions(UdonSharpBehaviour actionTarget, int priority)
        {
            if (actions == null)
            {
                actions = new UdonSharpBehaviour[1];
                actions[0] = actionTarget;
                priorities = new int[1];
                priorities[0] = priority;
            }
            else
            {
                int i = 0;
                while (i < actions.Length)
                {
                    if (priorities[i] > priority)
                    {
                        break;
                    }
                    i++;
                }
                actions = AddUdonSharpBehaviourArray(actions, actionTarget, i);
                priorities = AddIntArray(priorities, priority, i);
            }
        }

        private UdonSharpBehaviour[] AddUdonSharpBehaviourArray(UdonSharpBehaviour[] array, UdonSharpBehaviour value, int index)
        {
            UdonSharpBehaviour[] new_array = new UdonSharpBehaviour[array.Length + 1];
            array.CopyTo(new_array, 0);
            for (int i = 0; i < index; i++)
            {
                new_array[i] = array[i];
            }
            new_array[index] = value;
            for (int i = index + 1; i < new_array.Length; i++)
            {
                new_array[i] = array[i - 1];
            }
            return new_array;
        }

        private int[] AddIntArray(int[] array, int value, int index)
        {
            int[] new_array = new int[array.Length + 1];
            array.CopyTo(new_array, 0);
            for (int i = 0; i < index; i++)
            {
                new_array[i] = array[i];
            }
            new_array[index] = value;
            for (int i = index + 1; i < new_array.Length; i++)
            {
                new_array[i] = array[i - 1];
            }
            return new_array;
        }

        public virtual int GetSeed() { return -1; }
    }
}
