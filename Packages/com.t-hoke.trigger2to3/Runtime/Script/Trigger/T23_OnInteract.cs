
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnInteract : T23_TriggerBase
    {
        public override void Interact()
        {
            Trigger();
        }
    }
}
