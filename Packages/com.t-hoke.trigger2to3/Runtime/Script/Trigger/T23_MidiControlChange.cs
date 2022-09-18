
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Core.Config.Interfaces;
using VRC.Udon.Common;

namespace Trigger2to3
{
    public class T23_MidiControlChange : T23_TriggerBase
    {
        public int channel;

        public int number;

        public override void MidiControlChange(int _channel, int _number, int _velocity)
        {
            if (channel == _channel && number == _number)
            {
                Trigger();
            }
        }
    }
}
