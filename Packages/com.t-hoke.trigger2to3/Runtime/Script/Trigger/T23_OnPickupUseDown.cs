﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnPickupUseDown : T23_TriggerBase
    {
        public override void OnPickupUseDown()
        {
            Trigger();
        }
    }
}
