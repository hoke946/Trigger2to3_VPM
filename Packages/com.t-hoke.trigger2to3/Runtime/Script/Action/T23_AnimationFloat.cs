
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_AnimationFloat : T23_ActionBase
    {
        public Animator[] recievers;

        public string variable;

        public float operation;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                operation = propertyBox.value_f;
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
            target.SetFloat(variable, operation);
        }
    }
}
