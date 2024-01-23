
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetAvatarAudioParameters : T23_ActionBase
    {
        public bool triggeredPlayer = false;

        public float gain = 10;

        public float farRadius = 40;

        public float nearRadius = 0;

        public float volumetricRadius = 0;

        public bool forceSpatial = false;

        public bool customCurve = false;

        protected override void OnAction()
        {
            if (triggeredPlayer)
            {
                SetParameter(broadcast.triggeredPlayer);
            }
            else
            {
                VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
                VRCPlayerApi.GetPlayers(players);
                foreach (var player in players)
                {
                    if (player == null) { continue; }
                    SetParameter(player);
                }
            }
        }

        private void SetParameter(VRCPlayerApi player)
        {
            player.SetAvatarAudioGain(gain);
            player.SetAvatarAudioFarRadius(farRadius);
            player.SetAvatarAudioNearRadius(nearRadius);
            player.SetAvatarAudioVolumetricRadius(volumetricRadius);
            player.SetAvatarAudioForceSpatial(forceSpatial);
            player.SetAvatarAudioCustomCurve(customCurve);
        }
    }
}
