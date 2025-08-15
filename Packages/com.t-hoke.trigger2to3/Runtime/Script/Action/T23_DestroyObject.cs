
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_DestroyObject : T23_ActionBase
    {
        public GameObject[] recievers;
        
        protected override void OnAction()
        {
            for (int i = 0; i < recievers.Length; i++)
            {
                if (recievers[i])
                {
                    if (recievers[i])
                    {
                        Execute(recievers[i]);
                    }
                }
            }
        }

        private void Execute(GameObject target)
        {
            Destroy(target);
        }
    }
}
