
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_TakeOwnership : T23_ActionBase
    {
        public GameObject[] recievers;

        protected override void OnAction()
        {
            for (int i = 0; i < recievers.Length; i++)
            {
                if (recievers[i])
                {
                    Networking.SetOwner(Networking.LocalPlayer, recievers[i]);
                }
            }
        }
    }
}
