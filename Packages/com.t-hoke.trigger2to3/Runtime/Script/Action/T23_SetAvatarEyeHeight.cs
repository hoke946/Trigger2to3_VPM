
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetAvatarEyeHeight : T23_ActionBase
    {
        public float scaleMeters = 1.5f;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            Networking.LocalPlayer.SetAvatarEyeHeightByMeters(scaleMeters);
        }
    }
}
