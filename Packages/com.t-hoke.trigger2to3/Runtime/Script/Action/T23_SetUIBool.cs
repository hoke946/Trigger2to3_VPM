
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace Trigger2to3
{
    public class T23_SetUIBool : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool toggle;

        [Tooltip("if not toggle")]
        public bool operation;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                operation = propertyBox.value_b;
            }
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
            var uitoggle = target.GetComponent<Toggle>();
            if (uitoggle != null)
            {
                if (toggle)
                {
                    uitoggle.isOn = !uitoggle.isOn;
                }
                else
                {
                    uitoggle.isOn = operation;
                }
                return;
            }
        }
    }
}
