﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetAngularVelocity : T23_ActionBase
    {
        public Rigidbody[] recievers;

        public Vector3 angularVelocity;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool useWorldSpace;

        public bool takeOwnership;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                angularVelocity = propertyBox.value_v3;
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
                target.angularVelocity = angularVelocity;
            }
            else
            {
                target.angularVelocity = target.rotation * angularVelocity;
            }
        }
    }
}
