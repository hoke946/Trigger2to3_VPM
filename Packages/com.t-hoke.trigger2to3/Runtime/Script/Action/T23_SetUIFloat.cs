
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetUIFloat : T23_ActionBase
    {
        public GameObject[] recievers;

        public float operation;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool withoutNotify;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                operation = propertyBox.value_f;
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
            var slider = target.GetComponent<Slider>();
            if (slider != null)
            {
                if (withoutNotify)
                {
                    slider.SetValueWithoutNotify(operation);
                }
                else
                {
                    slider.value = operation;
                }
                return;
            }

            var scrollbar = target.GetComponent<Scrollbar>();
            if (scrollbar != null)
            {
                if (withoutNotify)
                {
                    scrollbar.SetValueWithoutNotify(operation);
                }
                else
                {
                    scrollbar.value = operation;
                }
                return;
            }
        }
    }
}
