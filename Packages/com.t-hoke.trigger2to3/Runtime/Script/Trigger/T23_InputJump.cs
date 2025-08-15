
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Core.Config.Interfaces;
using VRC.Udon.Common;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_InputJump : T23_TriggerBase
    {
        public bool inputValue = true;

        public override void InputJump(bool value, UdonInputEventArgs args)
        {
            if (value == inputValue)
            {
                Trigger();
            }
        }
    }
}
