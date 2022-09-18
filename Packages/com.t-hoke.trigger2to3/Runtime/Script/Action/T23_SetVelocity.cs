
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetVelocity : T23_ActionBase
    {
        public Rigidbody[] recievers;

        public Vector3 velocity;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool useWorldSpace;

        public bool takeOwnership;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                velocity = propertyBox.value_v3;
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
            if (useWorldSpace)
            {
                target.velocity = velocity;
            }
            else
            {
                target.velocity = target.rotation * velocity;
            }
        }
    }
}
