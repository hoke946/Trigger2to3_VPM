
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnEnable : T23_TriggerBase
    {
        private bool onEnabled = false;
        private bool firstFlame = true;

        void OnEnable()
        {
            onEnabled = true;
        }

        void Update()
        {
            if (firstFlame)
            {
                firstFlame = false;
                return;
            }

            if (onEnabled)
            {
                Trigger();
                onEnabled = false;
            }
        }
    }
}
