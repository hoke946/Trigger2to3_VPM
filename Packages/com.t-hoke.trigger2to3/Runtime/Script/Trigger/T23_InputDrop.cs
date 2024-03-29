﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Core.Config.Interfaces;
using VRC.Udon.Common;

namespace Trigger2to3
{
    public class T23_InputDrop : T23_TriggerBase
    {
        public bool inputValue = true;

        public int hand;

        public override void InputDrop(bool value, UdonInputEventArgs args)
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
