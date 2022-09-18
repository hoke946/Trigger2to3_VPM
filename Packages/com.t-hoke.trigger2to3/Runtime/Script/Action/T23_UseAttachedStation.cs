
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_UseAttachedStation : T23_ActionBase
    {
        protected override void OnAction()
        {
            Networking.LocalPlayer.UseAttachedStation();
        }
    }
}
