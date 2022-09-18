
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Trigger2to3
{
    public class T23_SpawnObject : T23_ActionBase
    {
        public GameObject prefab;

        public Transform[] locations;

        protected override void OnAction()
        {
            if (!prefab)
            {
                return;
            }

            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i])
                {
                    GameObject obj = Instantiate(prefab);
                    obj.transform.position = locations[i].position;
                    obj.transform.rotation = locations[i].rotation;
                }
            }
        }
    }
}
