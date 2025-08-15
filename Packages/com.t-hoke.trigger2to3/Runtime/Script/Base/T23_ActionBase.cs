
using UdonSharp;
using UnityEngine;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_ActionBase : T23_ModuleBase
    {
        public override int category { get { return 2; } }

        [SerializeField, Range(0, 1)]
        protected float randomAvg;

        protected float randomMin = 0;
        protected float randomMax = 0;

        protected T23_BroadcastBase broadcast;

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

            if (broadcast)
            {
                broadcast.AddActions(this, priority);

                if (broadcast.randomize)
                {
                    randomMin = broadcast.randomTotal;
                    broadcast.randomTotal += randomAvg;
                    randomMax = broadcast.randomTotal;
                }
            }

            this.enabled = false;
        }

        public void Action()
        {
            if (!RandomJudgement())
            {
                return;
            }

            OnAction();
        }

        protected virtual void OnAction() { }

        protected bool RandomJudgement()
        {
            if (broadcast)
            {
                if (!broadcast.randomize || (broadcast.randomValue >= randomMin && broadcast.randomValue < randomMax))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
