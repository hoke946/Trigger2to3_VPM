
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetParticlePlaying : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool toggle;

        [Tooltip("if not toggle")]
        public bool operation = true;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

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

        private void Execute(GameObject target)
        {
            ParticleSystem particle = target.GetComponent<ParticleSystem>();

            if (toggle)
            {
                if (particle.isPlaying)
                {
                    particle.Stop();
                }
                else
                {
                    particle.Play();
                }
            }
            else
            {
                if (operation)
                {
                    particle.Play();
                }
                else
                {
                    particle.Stop();
                }
            }
        }
    }
}
