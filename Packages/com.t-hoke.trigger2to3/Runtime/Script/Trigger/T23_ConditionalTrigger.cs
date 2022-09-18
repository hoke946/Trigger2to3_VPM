
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_ConditionalTrigger : T23_TriggerBase
    {
        public bool passive;

        public bool allowContinuity;

        public T23_PropertyBox basePropertyBox;

        public int compOperator;

        public int compParameterType;

        public T23_PropertyBox compPropertyBox;
        public bool comp_b;
        public int comp_i;
        public float comp_f;
        public Vector3 comp_v3;
        public string comp_s;

        private object value;
        private object before = null;

        private bool trigger_on;

        void Update()
        {
            if (!passive)
            {
                Judgement();
            }
        }

        public void Judgement()
        {
            if (basePropertyBox)
            {
                if (basePropertyBox.valueType == 0) { value = basePropertyBox.value_b; }
                if (basePropertyBox.valueType == 1) { value = basePropertyBox.value_i; }
                if (basePropertyBox.valueType == 2) { value = basePropertyBox.value_f; }
                if (basePropertyBox.valueType == 3) { value = basePropertyBox.value_v3; }
                if (basePropertyBox.valueType == 4) { value = basePropertyBox.value_s; }
            }
            if (compParameterType == 1)
            {
                if (compPropertyBox)
                {
                    if (basePropertyBox.valueType == 0) { comp_b = compPropertyBox.value_b; }
                    if (basePropertyBox.valueType == 1) { comp_i = compPropertyBox.value_i; }
                    if (basePropertyBox.valueType == 2) { comp_f = compPropertyBox.value_f; }
                    if (basePropertyBox.valueType == 3) { comp_v3 = compPropertyBox.value_v3; }
                    if (basePropertyBox.valueType == 4) { comp_s = compPropertyBox.value_s; }
                }
            }
            if (compParameterType == 2)
            {
                if (before != null)
                {
                    if (basePropertyBox.valueType == 0) { comp_b = (bool)before; }
                    if (basePropertyBox.valueType == 1) { comp_i = (int)before; }
                    if (basePropertyBox.valueType == 2) { comp_f = (float)before; }
                    if (basePropertyBox.valueType == 3) { comp_v3 = (Vector3)before; }
                    if (basePropertyBox.valueType == 4) { comp_s = (string)before; }
                }
            }

            bool on = false;
            if (compOperator == 0)
            {
                if (basePropertyBox.valueType == 0) { on = (bool)value == comp_b; }
                if (basePropertyBox.valueType == 1) { on = (int)value == comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value == comp_f; }
                if (basePropertyBox.valueType == 3) { on = (Vector3)value == comp_v3; }
                if (basePropertyBox.valueType == 4) { on = (string)value == comp_s; }
            }
            if (compOperator == 1)
            {
                if (basePropertyBox.valueType == 0) { on = (bool)value != comp_b; }
                if (basePropertyBox.valueType == 1) { on = (int)value != comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value != comp_f; }
                if (basePropertyBox.valueType == 3) { on = (Vector3)value != comp_v3; }
                if (basePropertyBox.valueType == 4) { on = (string)value != comp_s; }
            }
            if (compOperator == 2)
            {
                if (basePropertyBox.valueType == 1) { on = (int)value > comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value > comp_f; }
            }
            if (compOperator == 3)
            {
                if (basePropertyBox.valueType == 1) { on = (int)value < comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value < comp_f; }
            }
            if (compOperator == 4)
            {
                if (basePropertyBox.valueType == 1) { on = (int)value >= comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value >= comp_f; }
            }
            if (compOperator == 5)
            {
                if (basePropertyBox.valueType == 1) { on = (int)value <= comp_i; }
                if (basePropertyBox.valueType == 2) { on = (float)value <= comp_f; }
            }

            if (compParameterType == 2)
            {
                if (before == null) { on = true; }
                before = value;
            }

            if (on)
            {
                if (!trigger_on)
                {
                    Trigger();
                    if (!passive && !allowContinuity) { trigger_on = true; }
                }
            }
            else
            {
                trigger_on = false;
            }
        }
    }
}
