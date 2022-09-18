
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    public class T23_CallUdonMethod : T23_ActionBase
    {
        public UdonBehaviour udonBehaviour;

        public string method;

        public int ownershipControl;

        protected override void OnAction()
        {
            if (ownershipControl == 2)
            {
                Networking.SetOwner(Networking.LocalPlayer, udonBehaviour.gameObject);
            }
            Execute();
        }

        private void Execute()
        {
            if (ownershipControl == 1)
            {
                udonBehaviour.SendCustomNetworkEvent(NetworkEventTarget.Owner, method);
            }
            else
            {
                udonBehaviour.SendCustomEvent(method);
            }
        }
    }
}
