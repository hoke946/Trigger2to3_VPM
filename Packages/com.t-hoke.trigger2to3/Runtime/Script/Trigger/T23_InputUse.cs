
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Core.Config.Interfaces;
using VRC.Udon.Common;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_InputUse : T23_TriggerBase
    {
        public bool inputValue = true;

        public int hand;

        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            if (value == inputValue)
            {
                if (hand == 0 || (hand == 1 && args.handType == HandType.RIGHT) || (hand == 2 && args.handType == HandType.LEFT))
                {
                    Trigger();
                }
            }
        }
    }
}
