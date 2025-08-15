
using UnityEngine;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_ActiveConditionalTrigger : T23_ActionBase
    {
        public GameObject[] recievers;

        protected override void OnAction()
        {
            for (int i = 0; i < recievers.Length; i++)
            {
                if (recievers[i])
                {
                    Execute(recievers[i]);
                }
            }
        }

        private void Execute(GameObject target)
        {
            T23_ConditionalTrigger[] conditionalTriggers = target.GetComponents<T23_ConditionalTrigger>();
            for (int i = 0; i < conditionalTriggers.Length; i++)
            {
                conditionalTriggers[i].Judgement();
            }
        }
    }
}
