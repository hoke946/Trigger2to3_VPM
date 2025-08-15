
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetRandomChildActive : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool operation = true;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

        private int seedOffset = 100;

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
            int[] lottery = new int[target.transform.childCount];
            int inactiveCnt = 0;
            for (int cidx = 0; cidx < target.transform.childCount; cidx++)
            {
                if (target.transform.GetChild(cidx).gameObject.activeSelf != operation)
                {
                    lottery[inactiveCnt] = cidx;
                    inactiveCnt++;
                }
            }

            if (inactiveCnt == 0)
            {
                return;
            }

            if (broadcast)
            {
                int seed = broadcast.GetSeed();
                if (seed != -1)
                {
                    Random.InitState(seed + seedOffset);
                    seedOffset++;
                }
            }

            target.transform.GetChild(lottery[Random.Range(0, inactiveCnt)]).gameObject.SetActive(operation);
        }
    }
}
