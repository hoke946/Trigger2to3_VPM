
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_TeleportObject : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool byValue;

        public Transform teleportLocation;

        public bool local;

        public Vector3 teleportPosition;
        public T23_PropertyBox positionPropertyBox;
        public bool positionUsePropertyBox;

        public Vector3 teleportRotation;
        public T23_PropertyBox rotationPropertyBox;
        public bool rotationUsePropertyBox;

        public bool removeVelocity;

        public bool takeOwnership;

        protected override void OnAction()
        {
            if (byValue)
            {
                if (positionUsePropertyBox && positionPropertyBox)
                {
                    teleportPosition = positionPropertyBox.value_v3;
                }
                if (rotationUsePropertyBox && rotationPropertyBox)
                {
                    teleportRotation = rotationPropertyBox.value_v3;
                }
            }
            for (int i = 0; i < recievers.Length; i++)
            {
                if (recievers[i])
                {
                    if (takeOwnership)
                    {
                        Networking.SetOwner(Networking.LocalPlayer, recievers[i]);
                    }
                    Execute(recievers[i]);
                }
            }
        }

        private void Execute(GameObject target)
        {
            if (byValue)
            {
                if (local)
                {
                    target.transform.localPosition = teleportPosition;
                    target.transform.localRotation = Quaternion.Euler(teleportRotation);
                }
                else
                {
                    target.transform.position = teleportPosition;
                    target.transform.rotation = Quaternion.Euler(teleportRotation);
                }
            }
            else
            {
                target.transform.position = teleportLocation.position;
                target.transform.rotation = teleportLocation.rotation;
            }

            if (removeVelocity)
            {
                var rigidBody = target.GetComponent<Rigidbody>();
                if (rigidBody)
                {
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}
