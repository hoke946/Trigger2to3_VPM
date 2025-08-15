
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Core.Config.Interfaces;
using VRC.Udon.Common;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_MidiNoteOn : T23_TriggerBase
    {
        public int channel;

        public int note = 21;

        public override void MidiNoteOn(int _channel, int _number, int _velocity)
        {
            if (channel == _channel && note == _number)
            {
                Trigger();
            }
        }
    }
}
