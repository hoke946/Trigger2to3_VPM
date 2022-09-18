
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_AudioPlay : T23_ActionBase
    {
        public AudioSource[] recievers;

        public bool toggle;

        [Tooltip("if not toggle")]
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
            if (toggle)
            {
                if (target.isPlaying)
                {
                    target.Stop();
                }
                else
                {
                    target.Play();
                }
            }
            else
            {
                if (operation)
                {
                    target.Play();
                }
                else
                {
                    target.Stop();
                }
            }
        }
    }
}
