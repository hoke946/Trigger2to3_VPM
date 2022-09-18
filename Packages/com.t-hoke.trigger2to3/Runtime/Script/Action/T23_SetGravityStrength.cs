
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetGravityStrength : T23_ActionBase
    {
        public float gravityStrength = 1;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                gravityStrength = propertyBox.value_f;
            }
            Networking.LocalPlayer.SetGravityStrength(gravityStrength);
        }
    }
}
