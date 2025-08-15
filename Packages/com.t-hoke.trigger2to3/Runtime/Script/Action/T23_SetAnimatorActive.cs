
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetAnimatorActive : T23_ActionBase
    {
        public Animator[] recievers;

        public bool toggle;

        [Tooltip("if not toggle")]
        public bool operation = true;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                operation = propertyBox.value_b;
            }
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
            if (toggle)
            {
                if (target)
                {
                    target.enabled = !target.enabled;
                }
            }
            else
            {
                if (target)
                {
                    target.enabled = operation;
                }
            }
        }
    }
}
