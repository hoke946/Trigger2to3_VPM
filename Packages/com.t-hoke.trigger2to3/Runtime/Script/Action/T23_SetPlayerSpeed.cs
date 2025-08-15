
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_SetPlayerSpeed : T23_ActionBase
    {
        public float walkSpeed = 2;
        public T23_PropertyBox propertyBox_walk;
        public bool usePropertyBox_walk;

        public float runSpeed = 4;
        public T23_PropertyBox propertyBox_run;
        public bool usePropertyBox_run;

        public float strafeSpeed = 2;
        public T23_PropertyBox propertyBox_strafe;
        public bool usePropertyBox_strafe;

        protected override void OnAction()
        {
            if (usePropertyBox_walk && propertyBox_walk)
            {
                walkSpeed = propertyBox_walk.value_f;
            }
            if (usePropertyBox_run && propertyBox_run)
            {
                runSpeed = propertyBox_run.value_f;
            }
            if (usePropertyBox_strafe && propertyBox_strafe)
            {
                strafeSpeed = propertyBox_strafe.value_f;
            }
            Networking.LocalPlayer.SetWalkSpeed(walkSpeed);
            Networking.LocalPlayer.SetRunSpeed(runSpeed);
            Networking.LocalPlayer.SetStrafeSpeed(strafeSpeed);
        }
    }
}
