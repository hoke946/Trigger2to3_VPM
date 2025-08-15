
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetJumpImpulse : T23_ActionBase
    {
        public float impulse = 3;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                impulse = propertyBox.value_f;
            }
            Networking.LocalPlayer.SetJumpImpulse(impulse);
        }
    }
}
