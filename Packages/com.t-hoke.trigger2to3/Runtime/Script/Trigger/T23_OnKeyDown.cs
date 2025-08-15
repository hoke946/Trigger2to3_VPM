
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnKeyDown : T23_TriggerBase
    {
        public KeyCode key;

        public string keyFree;

        void Update()
        {
            if (keyFree == "")
            {
                if (Input.GetKeyDown(key))
                {
                    Trigger();
                }
            }
            else
            {
                if (Input.GetKeyDown(keyFree))
                {
                    Trigger();
                }
            }
        }
    }
}
