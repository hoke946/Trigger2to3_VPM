
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.Components;

namespace Trigger2to3
{
    public class T23_UseAudioBank : T23_ActionBase
    {
        public T23_AudioBank audioBank;

        public int command;

        public int index;

        protected override void OnAction()
        {
            if (!audioBank)
            {
                return;
            }

            if (broadcast)
            {
                int seed = broadcast.GetSeed();
                if (seed != -1)
                {
                    audioBank.SetInitialSeed(broadcast.GetSeed() + 20000);
                }
            }

            switch (command)
            {
                case 0:
                    audioBank.Play(index);
                    break;
                case 1:
                    audioBank.Stop();
                    break;
                case 2:
                    audioBank.PlayNext();
                    break;
                case 3:
                    audioBank.Shuffle();
                    break;
            }
        }
    }
}
