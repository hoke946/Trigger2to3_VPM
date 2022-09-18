
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace Trigger2to3
{

    public class T23_SetAvatarUse : T23_ActionBase
    {
        protected override void OnAction()
        {
            var pedestal = (VRCAvatarPedestal)GetComponent(typeof(VRCAvatarPedestal));
            pedestal.SetAvatarUse(Networking.LocalPlayer);
        }
    }
}
