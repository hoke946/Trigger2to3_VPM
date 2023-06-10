
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace Trigger2to3
{
    public class T23_UIOnValueChanged : T23_TriggerBase
    {
        public Object UIComponent;
        public string componentType;

        public bool any = true;
        public bool isOn = false;
        public int value = 0;

        public void OnValueChanged()
        {
            if (componentType == typeof(Toggle).ToString())
            {
                var toggle = (Toggle)UIComponent;
                if (any || (toggle.isOn == isOn))
                {
                    Trigger();
                }
            }
            else if (componentType == typeof(Dropdown).ToString())
            {
                var dropdown = (Dropdown)UIComponent;
                if (any || (dropdown.value == value))
                {
                    Trigger();
                }
            }
            else
            {
                Trigger();
            }
        }
    }
}
