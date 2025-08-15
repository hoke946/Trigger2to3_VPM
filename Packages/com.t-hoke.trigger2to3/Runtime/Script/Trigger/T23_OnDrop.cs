
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnDrop : T23_TriggerBase
    {
        public override void OnDrop()
        {
            Trigger();
        }
    }
}
