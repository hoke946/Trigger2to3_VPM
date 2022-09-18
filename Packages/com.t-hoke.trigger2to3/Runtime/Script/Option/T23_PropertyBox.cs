
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace Trigger2to3
{
    public class T23_PropertyBox : UdonSharpBehaviour
    {
        public int valueType;

        public bool value_b;
        public int value_i;
        public float value_f;
        public Vector3 value_v3;
        public string value_s;

        public int trackType;

        public GameObject targetObject;

        public int targetPlayer;

        public T23_TriggerBase targetTrigger;

        public Object targetComponent;

        public int index;

        public string spot;

        public string spotDetail;

        public bool positionTracking;

        public bool updateEveryFrame;

        private System.DateTime startTime;

        void Start()
        {
            startTime = System.DateTime.Now;
        }

        void Update()
        {
            if (updateEveryFrame)
            {
                UpdateTrackValue();
            }
        }

        public void UpdateTrackValue()
        {
            if (trackType == 1)
            {
                VRCPlayerApi player = null;
                switch (targetPlayer)
                {
                    case 0:
                        player = Networking.LocalPlayer;
                        break;
                    case 1:
                        player = Networking.GetOwner(gameObject);
                        break;
                    case 2:
                        if (targetTrigger)
                        {
                            var playerField = targetTrigger.triggeredPlayer;
                            player = playerField;
                        }
                        break;
                    case 3:
                        VRCPlayerApi[] players = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()]; ;
                        VRCPlayerApi.GetPlayers(players);
                        if (index < players.Length)
                        {
                            player = players[index];
                        }
                        else
                        {
                            player = Networking.LocalPlayer;
                        }
                        break;
                }
                if (player == null || !player.IsValid()) { return; }

                switch (spot)
                {
                    case "IsUserInVR":
                        value_b = player.IsUserInVR();
                        break;
                    case "IsPlayerGrounded":
                        value_b = player.IsPlayerGrounded();
                        break;
                    case "IsMaster":
                        value_b = player.isMaster;
                        break;
                    case "IsInstanceOwner":
                        value_b = player.isInstanceOwner;
                        break;
                    case "IsGameObjectOwner":
                        value_b = player.IsOwner(gameObject);
                        break;
                    case "Position":
                        value_v3 = player.GetPosition();
                        if (positionTracking)
                        {
                            transform.position = player.GetPosition();
                            transform.rotation = player.GetRotation();
                        }
                        break;
                    case "Rotation":
                        value_v3 = player.GetRotation().eulerAngles;
                        break;
                    case "HeadPosition":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                        if (positionTracking)
                        {
                            transform.position = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                            transform.rotation = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                        }
                        break;
                    case "HeadRotation":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation.eulerAngles;
                        break;
                    case "RightHandPosition":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
                        if (positionTracking)
                        {
                            transform.position = player.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
                            transform.rotation = player.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).rotation;
                        }
                        break;
                    case "RightHandRotation":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).rotation.eulerAngles;
                        break;
                    case "LeftHandPosition":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
                        if (positionTracking)
                        {
                            transform.position = player.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
                            transform.rotation = player.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).rotation;
                        }
                        break;
                    case "LeftHandRotation":
                        value_v3 = player.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).rotation.eulerAngles;
                        break;
                    case "Velocity":
                        value_v3 = player.GetVelocity();
                        break;
                    case "DisplayName":
                        value_s = player.displayName;
                        break;
                }
            }

            if (trackType == 2)
            {
                if (!targetObject) { return; }

                switch (spot)
                {
                    case "IsActive":
                        value_b = targetObject.activeSelf;
                        break;
                    case "Position":
                        value_v3 = targetObject.transform.position;
                        if (positionTracking)
                        {
                            transform.position = targetObject.transform.position;
                            transform.rotation = targetObject.transform.rotation;
                        }
                        break;
                    case "Rotation":
                        value_v3 = targetObject.transform.rotation.eulerAngles;
                        break;
                    case "LocalPosition":
                        value_v3 = targetObject.transform.localPosition;
                        if (positionTracking)
                        {
                            transform.position = targetObject.transform.position;
                            transform.rotation = targetObject.transform.rotation;
                        }
                        break;
                    case "LocalRotation":
                        value_v3 = targetObject.transform.localRotation.eulerAngles;
                        break;
                    case "Velocity":
                        value_v3 = targetObject.GetComponent<Rigidbody>().velocity;
                        break;
                    case "AngularVelocity":
                        value_v3 = targetObject.GetComponent<Rigidbody>().angularVelocity;
                        break;
                }
            }

            if (trackType == 3)
            {
                switch (spot)
                {
                    case "PlayerCount":
                        value_i = VRCPlayerApi.GetPlayerCount();
                        value_f = value_i;
                        break;
                    case "Year":
                        value_i = System.DateTime.Now.Year;
                        value_f = value_i;
                        break;
                    case "Month":
                        value_i = System.DateTime.Now.Month;
                        value_f = value_i;
                        break;
                    case "Day":
                        value_i = System.DateTime.Now.Day;
                        value_f = value_i;
                        break;
                    case "DayOfWeek":
                        value_i = (int)System.DateTime.Now.DayOfWeek;
                        value_f = value_i;
                        break;
                    case "Hour":
                        value_i = System.DateTime.Now.Hour;
                        value_f = value_i;
                        break;
                    case "Minute":
                        value_i = System.DateTime.Now.Minute;
                        value_f = value_i;
                        break;
                    case "Second":
                        value_i = System.DateTime.Now.Second;
                        value_f = value_i;
                        break;
                    case "JoinHours":
                        value_f = (float)(System.DateTime.Now - startTime).TotalHours;
                        value_i = (int)value_f;
                        break;
                    case "JoinMinutes":
                        value_f = (float)(System.DateTime.Now - startTime).TotalMinutes;
                        value_i = (int)value_f;
                        break;
                    case "JoinSeconds":
                        value_f = (float)(System.DateTime.Now - startTime).TotalSeconds;
                        value_i = (int)value_f;
                        break;
                }
            }

            if (trackType == 4)
            {
                if (targetComponent)
                {
                    if (valueType == 0)
                    {
                        if (spot == "Toggle")
                        {
                            var toggle = (Toggle)targetComponent;
                            value_b = toggle.isOn;
                        }
                    }
                    if (valueType == 1)
                    {
                        if (spot == "Text")
                        {
                            var text = (Text)targetComponent;
                            int.TryParse(text.text, out value_i);
                        }
                        if (spot == "InputField")
                        {
                            var inputField = (InputField)targetComponent;
                            int.TryParse(inputField.text, out value_i);
                        }
                        if (spot == "Dropdown")
                        {
                            var dropdown = (Dropdown)targetComponent;
                            value_i = dropdown.value;
                        }
                    }
                    if (valueType == 2)
                    {
                        if (spot == "Slider")
                        {
                            var slider = (Slider)targetComponent;
                            value_f = slider.value;
                        }
                        if (spot == "Scrollbar")
                        {
                            var scrollbar = (Scrollbar)targetComponent;
                            value_f = scrollbar.value;
                        }
                        if (spot == "Text")
                        {
                            var text = (Text)targetComponent;
                            float.TryParse(text.text, out value_f);
                        }
                        if (spot == "InputField")
                        {
                            var inputField = (InputField)targetComponent;
                            float.TryParse(inputField.text, out value_f);
                        }
                        if (spot == "Dropdown")
                        {
                            var dropdown = (Dropdown)targetComponent;
                            value_f = dropdown.value;
                        }
                    }
                    if (valueType == 4)
                    {
                        if (spot == "Text")
                        {
                            var text = (Text)targetComponent;
                            value_s = text.text;
                        }
                        if (spot == "InputField")
                        {
                            var inputField = (InputField)targetComponent;
                            value_s = inputField.text;
                        }
                    }
                }
            }

            if (trackType == 5)
            {
                if (targetComponent != null && spot != "")
                {
                    Animator animator = (Animator)targetComponent;
                    if (valueType == 0)
                    {
                        value_b = animator.GetBool(spot);
                    }
                    if (valueType == 1)
                    {
                        value_i = animator.GetInteger(spot);
                    }
                    if (valueType == 2)
                    {
                        value_f = animator.GetFloat(spot);
                    }
                }
            }

            if (trackType == 6)
            {
                switch (spot)
                {
                    case "RightIndexTrigger":
                        value_f = Input.GetAxis("Oculus_CrossPlatform_SecondaryIndexTrigger");
                        break;
                    case "LeftIndexTrigger":
                        value_f = Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger");
                        break;
                    case "RightGrip":
                        value_f = Input.GetAxis("Oculus_CrossPlatform_SecondaryHandTrigger");
                        break;
                    case "LeftGrip":
                        value_f = Input.GetAxis("Oculus_CrossPlatform_PrimaryHandTrigger");
                        break;
                }
            }

            UpdateSubValue();
        }

        public void UpdateSubValue()
        {
            switch (spotDetail)
            {
                case "X":
                    value_f = value_v3.x;
                    break;
                case "Y":
                    value_f = value_v3.y;
                    break;
                case "Z":
                    value_f = value_v3.z;
                    break;
                case "Magnitude":
                    value_f = value_v3.magnitude;
                    break;
                case "OneLetter":
                    if (index < value_s.Length) { value_s = value_s.Substring(index, 1); }
                    else { value_s = ""; }
                    break;
            }

            switch (valueType)
            {
                case 0:
                    value_s = value_b.ToString();
                    break;
                case 1:
                    value_s = value_i.ToString();
                    break;
                case 2:
                    value_s = value_f.ToString();
                    break;
                case 3:
                    value_s = value_v3.ToString();
                    break;
            }
        }

        public override void InputMoveHorizontal(float value, UdonInputEventArgs args)
        {
            if (trackType == 4 && spot == "MoveHorizontal")
            {
                value_f = value;
            }
        }

        public override void InputMoveVertical(float value, UdonInputEventArgs args)
        {
            if (trackType == 4 && spot == "MoveVertical")
            {
                value_f = value;
            }
        }

        public override void InputLookHorizontal(float value, UdonInputEventArgs args)
        {
            if (trackType == 4 && spot == "LookHorizontal")
            {
                value_f = value;
            }
        }

        public override void InputLookVertical(float value, UdonInputEventArgs args)
        {
            if (trackType == 4 && spot == "LookVertical")
            {
                value_f = value;
            }
        }
    }
}
