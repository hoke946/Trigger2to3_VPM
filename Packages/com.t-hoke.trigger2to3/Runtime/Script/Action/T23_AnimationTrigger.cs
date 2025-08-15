
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_AnimationTrigger : T23_ActionBase
    {
        public Animator[] recievers;

        public string trigger;

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

        private void Execute(Animator target)
        {
            target.SetTrigger(trigger);
        }
    }
}
