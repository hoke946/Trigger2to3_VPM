
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace Trigger2to3
{
    public class T23_SetRendererActive : T23_ActionBase
    {
        public Renderer[] recievers;

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

        private void Execute(Renderer target)
        {
            if (toggle)
            {
                if (target)
                {
                    target.enabled = !target.enabled;
                }
            }
            else
            {
                if (target)
                {
                    target.enabled = operation;
                }
            }
        }
    }
}
