
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_ActiveCustomTrigger : T23_ActionBase
    {
        public GameObject[] recievers;

        public string Name;

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
            T23_CustomTrigger[] customTriggers = target.GetComponents<T23_CustomTrigger>();
            for (int i = 0; i < customTriggers.Length; i++)
            {
                if (customTriggers[i].Name == Name)
                {
                    customTriggers[i].Trigger();
                }
            }
        }
    }
}
