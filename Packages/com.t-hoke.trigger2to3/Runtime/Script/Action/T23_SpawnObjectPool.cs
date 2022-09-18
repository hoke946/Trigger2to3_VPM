
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.Components;

namespace Trigger2to3
{
    public class T23_SpawnObjectPool : T23_ActionBase
    {
        public VRCObjectPool objectPool;

        protected override void OnAction()
        {
            Networking.SetOwner(Networking.LocalPlayer, objectPool.gameObject);

            objectPool.TryToSpawn();
        }
    }
}
