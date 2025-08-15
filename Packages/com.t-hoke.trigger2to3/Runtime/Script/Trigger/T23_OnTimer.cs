
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_OnTimer : T23_TriggerBase
    {
        public bool repeat;

        public bool resetOnEnable;

        public float lowPeriodTime;

        public float highPeriodTime;

        private float timer;
        private float nextPeriodTime;
        private bool finished;

        void Update()
        {
            if (finished) { return; }

            timer += Time.deltaTime;

            if (timer > nextPeriodTime)
            {
                Trigger();

                if (repeat)
                {
                    ResetTime();
                }
                else
                {
                    finished = true;
                    this.enabled = false;
                }
            }
        }

        private void ResetTime()
        {
            finished = false;
            this.enabled = true;
            timer = 0;
            if (highPeriodTime > lowPeriodTime)
            {
                nextPeriodTime = Random.Range(lowPeriodTime, highPeriodTime);
            }
            else
            {
                nextPeriodTime = lowPeriodTime;
            }
        }

        private void OnEnable()
        {
            if (resetOnEnable)
            {
                ResetTime();
            }
        }
    }
}
