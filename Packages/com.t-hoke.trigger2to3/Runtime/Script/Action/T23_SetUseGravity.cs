
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetUseGravity : T23_ActionBase
    {
        public Rigidbody[] recievers;

        public bool operation = true;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool takeOwnership;

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
                    if (takeOwnership)
                    {
                        Networking.SetOwner(Networking.LocalPlayer, recievers[i].gameObject);
                    }
                    Execute(recievers[i]);
                }
            }
        }

        private void Execute(Rigidbody target)
        {
            target.useGravity = operation;
        }
    }
}
