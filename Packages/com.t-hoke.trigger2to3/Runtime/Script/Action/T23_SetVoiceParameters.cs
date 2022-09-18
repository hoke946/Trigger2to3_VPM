
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    public class T23_SetVoiceParameters : T23_ActionBase
    {
        public bool triggeredPlayer = false;

        public float distanceFar = 25;

        public float distanceNear = 0;

        public float gain = 15;

        public bool lowpass = true;

        public float volumetricRadius = 0;

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
            player.SetVoiceDistanceFar(distanceFar);
            player.SetVoiceDistanceNear(distanceNear);
            player.SetVoiceGain(gain);
            player.SetVoiceLowpass(lowpass);
            player.SetVoiceVolumetricRadius(volumetricRadius);
        }
    }
}
