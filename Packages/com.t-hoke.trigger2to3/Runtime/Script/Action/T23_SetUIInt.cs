
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetUIInt : T23_ActionBase
    {
        public GameObject[] recievers;

        public int operation;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        public bool withoutNotify;

        protected override void OnAction()
        {
            if (usePropertyBox && propertyBox)
            {
                operation = propertyBox.value_i;
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
            var dropdown = target.GetComponent<Dropdown>();
            if (dropdown != null)
            {
                if (withoutNotify)
                {
                    dropdown.SetValueWithoutNotify(operation);
                }
                else
                {
                    dropdown.value = operation;
                }
                return;
            }
        }
    }
}
