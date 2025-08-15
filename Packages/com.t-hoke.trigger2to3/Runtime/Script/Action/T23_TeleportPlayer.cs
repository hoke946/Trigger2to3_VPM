
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [AddComponentMenu("")]
    public class T23_TeleportPlayer : T23_ActionBase
    {
        public bool byValue;

        public Transform teleportLocation;

        public Vector3 teleportPosition;
        public T23_PropertyBox positionPropertyBox;
        public bool positionUsePropertyBox;

        public Vector3 teleportRotation;
        public T23_PropertyBox rotationPropertyBox;
        public bool rotationUsePropertyBox;

        public VRC_SceneDescriptor.SpawnOrientation teleportOrientation;

        public bool lerpOnRemote;

        protected override void OnAction()
        {
            if (byValue)
            {
                Networking.LocalPlayer.TeleportTo(teleportPosition, Quaternion.Euler(teleportRotation), teleportOrientation, lerpOnRemote);
            }
            else
            {
                Networking.LocalPlayer.TeleportTo(teleportLocation.position, teleportLocation.rotation, teleportOrientation, lerpOnRemote);
            }
        }
    }
}
