
using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("Trigger2to3/Trigger2to3 ConnectFromUdon (T23_ConnectFromUdon)")]
    public class T23_ConnectFromUdon : UdonSharpBehaviour
    {
        public GameObject target;
        public string customTriggerName;

        private T23_CustomTrigger targetTrigger;

        void Start()
        {
            this.enabled = false;
        }

        public void ActiveCustomTrigger()
        {
            if (!target) { return; }
            if (targetTrigger)
            {
                targetTrigger.Trigger();
            }
            else
            {
                T23_CustomTrigger[] customTriggers = target.GetComponents<T23_CustomTrigger>();
                for (int i = 0; i < customTriggers.Length; i++)
                {
                    if (customTriggers[i].Name == customTriggerName)
                    {
                        customTriggers[i].Trigger();
                        targetTrigger = customTriggers[i];
                        return;
                    }
                }
            }
        }
    }
}
