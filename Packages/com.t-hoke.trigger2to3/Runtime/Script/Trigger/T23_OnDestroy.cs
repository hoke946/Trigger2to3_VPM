﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_OnDestroy : T23_TriggerBase
    {
        public void OnDestroy()
        {
            Trigger();
        }
    }
}
