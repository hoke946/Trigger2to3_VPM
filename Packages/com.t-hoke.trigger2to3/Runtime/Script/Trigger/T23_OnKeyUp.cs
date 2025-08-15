
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnKeyUp : T23_TriggerBase
    {
        public KeyCode key;

        public string keyFree;

        void Update()
        {
            if (keyFree == "")
            {
                if (Input.GetKeyUp(key))
                {
                    Trigger();
                }
            }
            else
            {
                if (Input.GetKeyUp(keyFree))
                {
                    Trigger();
                }
            }
        }
    }
}
