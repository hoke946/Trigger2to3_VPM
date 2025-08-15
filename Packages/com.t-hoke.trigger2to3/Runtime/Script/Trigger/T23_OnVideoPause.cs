
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnVideoPause : T23_TriggerBase
    {
        public override void OnVideoPause()
        {
            Trigger();
        }
    }
}
