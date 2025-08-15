
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_AudioPause : T23_ActionBase
    {
        public AudioSource[] recievers;

        public bool operation = true;

        protected override void OnAction()
        {
            for (int i = 0; i < recievers.Length; i++)
            {
                if (recievers[i])
                {
                    Execute(recievers[i]);
                }
            }
        }

        private void Execute(AudioSource target)
        {
            if (operation)
            {
                target.Pause();
            }
            else
            {
                target.UnPause();
            }
        }
    }
}
