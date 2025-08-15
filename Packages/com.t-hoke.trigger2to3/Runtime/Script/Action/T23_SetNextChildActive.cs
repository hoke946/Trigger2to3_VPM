
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetNextChildActive : T23_ActionBase
    {
        public GameObject[] recievers;

        public bool operation = true;
        public T23_PropertyBox propertyBox;
        public bool usePropertyBox;

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
            for (int cidx = 0; cidx < target.transform.childCount; cidx++)
            {
                if (target.transform.GetChild(cidx).gameObject.activeSelf != operation)
                {
                    target.transform.GetChild(cidx).gameObject.SetActive(operation);
                    break;
                }
            }
        }
    }
}
