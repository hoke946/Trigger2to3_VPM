
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using VRC.SDK3.Components;

namespace Trigger2to3
{
    public class T23_SetPropertyBox : T23_ActionBase
    {
        public T23_PropertyBox propertyBox;

        public int calcOperator;

        public bool value_bool;
        public int value_int;
        public float value_float;
        public Vector3 value_Vector3;
        public string value_string;
        public T23_PropertyBox valuePropertyBox;
        public bool usePropertyBox;

        protected override void OnAction()
        {
            if (!propertyBox) { return; }

            if (usePropertyBox && valuePropertyBox)
            {
                value_bool = valuePropertyBox.value_b;
                value_int = valuePropertyBox.value_i;
                value_float = valuePropertyBox.value_f;
                value_Vector3 = valuePropertyBox.value_v3;
                value_string = valuePropertyBox.value_s;
            }
            if (calcOperator == 0)
            {
                propertyBox.UpdateTrackValue();
            }
            if (calcOperator == 1)
            {
                if (propertyBox.valueType == 0) { propertyBox.value_b = value_bool; }
                if (propertyBox.valueType == 1) { propertyBox.value_i = value_int; }
                if (propertyBox.valueType == 2) { propertyBox.value_f = value_float; }
                if (propertyBox.valueType == 3) { propertyBox.value_v3 = value_Vector3; }
                if (propertyBox.valueType == 4) { propertyBox.value_s = value_string; }
            }
            if (calcOperator == 2)
            {
                if (propertyBox.valueType == 1) { propertyBox.value_i += value_int; }
                if (propertyBox.valueType == 2) { propertyBox.value_f += value_float; }
                if (propertyBox.valueType == 3) { propertyBox.value_v3 += value_Vector3; }
            }
            if (calcOperator == 3)
            {
                if (propertyBox.valueType == 1) { propertyBox.value_i -= value_int; }
                if (propertyBox.valueType == 2) { propertyBox.value_f -= value_float; }
                if (propertyBox.valueType == 3) { propertyBox.value_v3 -= value_Vector3; }
            }
            if (calcOperator == 4)
            {
                if (propertyBox.valueType == 1) { propertyBox.value_i *= value_int; }
                if (propertyBox.valueType == 2) { propertyBox.value_f *= value_float; }
                if (propertyBox.valueType == 3) { propertyBox.value_v3 *= value_float; }
            }
            if (calcOperator == 5)
            {
                if (propertyBox.valueType == 1) { propertyBox.value_i /= value_int; }
                if (propertyBox.valueType == 2) { propertyBox.value_f /= value_float; }
                if (propertyBox.valueType == 3) { propertyBox.value_v3 /= value_float; }
            }
            propertyBox.UpdateSubValue();
        }
    }
}
