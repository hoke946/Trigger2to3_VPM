
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetUIBool : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool toggle;

        [Tooltip("if not toggle")]
        public bool operation;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool withoutNotify;

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
                bool isOn = toggle ? !uitoggle.isOn : operation;
                if (withoutNotify)
                {
                    uitoggle.SetIsOnWithoutNotify(isOn);
                }
                else
                {
                    uitoggle.isOn = isOn;
                }
                return;
            }
        }
    }
}
