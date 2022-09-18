
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetPlayerVelocity : T23_ActionBase
    {
        public Vector3 velocity;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                velocity = propertyBox.value_v3;
            }
            Networking.LocalPlayer.SetVelocity(velocity);
        }
    }
}
