
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_ModuleBase : UdonSharpBehaviour
    {
        public int groupID;
        public int priority;
        public string title;
        public virtual int category { get; }

        void Start()
        {
            OnStart();
            PostStart();
        }

        protected virtual void OnStart() { }

        protected virtual void PostStart() { }
    }
}
