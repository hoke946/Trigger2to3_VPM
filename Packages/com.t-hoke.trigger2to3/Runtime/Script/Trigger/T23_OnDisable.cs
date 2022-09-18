
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnDisable : T23_TriggerBase
    {
        private void OnDisable()
        {
            Trigger();
        }
    }
}
