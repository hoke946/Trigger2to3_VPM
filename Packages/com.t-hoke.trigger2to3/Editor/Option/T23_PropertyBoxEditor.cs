#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UdonSharpEditor;
using System.Collections.Generic;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_PropertyBox))]
    internal class T23_PropertyBoxEditor : Editor
    {
        T23_PropertyBox body;

        SerializedProperty prop;

        public enum ValueType
        {
            Bool = 0,
            Int = 1,
            Float = 2,
            Vector3 = 3,
            String = 4
        }

        public enum TrackType
        {
            None = 0,
            Player = 1,
            GameObject = 2,
            World = 3,
            UI = 4,
            AnimatorParameter = 5,
            Controller = 6,
        }

        public enum TargetPlayer
        {
            Local = 0,
            ObjectOwner = 1,
            TriggeredPlayer = 2,
            ByIndex = 3
        }

        private string[] PlayerSpot_b = { "IsUserInVR", "IsPlayerGrounded", "IsMaster", "IsInstanceOwner", "IsGameObjectOwner" };
        private string[] PlayerSpot_v3 = { "Position", "Rotation", "HeadPosition", "HeadRotation", "RightHandPosition", "RightHandRotation", "LeftHandPosition", "LeftHandRotation", "Velocity" };
        private string[] PlayerSpot_s = { "DisplayName" };

        private string[] ObjectSpot_b = { "IsActive" };
        private string[] ObjectSpot_v3 = { "Position", "Rotation", "LocalPosition", "LocalRotation", "Velocity", "AngularVelocity" };

        private string[] WorldSpot_if = { "PlayerCount", "Year", "Month", "Day", "DayOfWeek", "Hour", "Minute", "Second", "JoinHours", "JoinMinutes", "JoinSeconds" };

        private string[] ControllerSpot_f = { "RightIndexTrigger", "LeftIndexTrigger", "RightGrip", "LeftGrip", "MoveHorizo​​ntal", "MoveVertical", "LookHorizontal", "LookVertical" };

        private string[] SpotDetail_v3_f = { "X", "Y", "Z", "Magnitude" };
        private string[] SpotDetail_s = { "Nomal", "OneLetter" };

        void OnEnable()
        {
            body = target as T23_PropertyBox;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            T23_EditorUtility.ShowTitle("Option");
            GUILayout.Box("PropertyBox", T23_EditorUtility.HeadlineStyle());

            UdonSharpGUI.DrawCompileErrorTextArea();

            serializedObject.Update();

            serializedObject.FindProperty("valueType").intValue = (int)(ValueType)EditorGUILayout.EnumPopup("Value Type", (ValueType)body.valueType);
            if (body.valueType == 0)
            {
                serializedObject.FindProperty("value_b").boolValue = EditorGUILayout.Toggle("Value", body.value_b);
            }
            else if (body.valueType == 1)
            {
                serializedObject.FindProperty("value_i").intValue = EditorGUILayout.IntField("Value", body.value_i);
            }
            else if (body.valueType == 2)
            {
                serializedObject.FindProperty("value_f").floatValue = EditorGUILayout.FloatField("Value", body.value_f);
            }
            else if (body.valueType == 3)
            {
                serializedObject.FindProperty("value_v3").vector3Value = EditorGUILayout.Vector3Field("Value", body.value_v3);
            }
            else if (body.valueType == 4)
            {
                serializedObject.FindProperty("value_s").stringValue = EditorGUILayout.TextField("Value", body.value_s);
            }

            UdonSharpGUI.DrawUILine(Color.gray);

            serializedObject.FindProperty("trackType").intValue = (int)(TrackType)EditorGUILayout.EnumPopup("Track Type", (TrackType)body.trackType);
            if (body.trackType != 0)
            {
                List<string> spotList = new List<string>();
                if (body.trackType == 1)
                {
                    serializedObject.FindProperty("targetPlayer").intValue = (int)(TargetPlayer)EditorGUILayout.EnumPopup("Target Player", (TargetPlayer)body.targetPlayer);

                    if (body.targetPlayer == 1 || body.targetPlayer == 2)
                    {
                        prop = serializedObject.FindProperty("targetObject");
                        EditorGUILayout.PropertyField(prop);
                    }

                    if (body.targetPlayer == 2)
                    {
                        List<string> triggerList = new List<string>();
                        List<T23_TriggerBase> triggerComponentList = new List<T23_TriggerBase>();
                        if (body.targetObject)
                        {
                            var triggers = body.targetObject.GetComponents<T23_TriggerBase>();
                            foreach (var trigger in triggers)
                            {
                                if (trigger.playerTrigger)
                                {
                                    triggerComponentList.Add(trigger);
                                    triggerList.Add($"[#{trigger.groupID}] {trigger.title}");
                                }
                            }
                        }
                        var triggerIndex = EditorGUILayout.Popup("Target Trigger", triggerComponentList.IndexOf(body.targetTrigger), triggerList.ToArray());
                        serializedObject.FindProperty("targetTrigger").objectReferenceValue = triggerIndex >= 0 ? triggerComponentList[triggerIndex] : null;
                    }

                    if (body.targetPlayer == 3)
                    {
                        prop = serializedObject.FindProperty("index");
                        EditorGUILayout.PropertyField(prop);
                    }

                    if (body.valueType == 0)
                    {
                        spotList.AddRange(PlayerSpot_b);
                    }
                    else if (body.valueType == 2 || body.valueType == 3)
                    {
                        spotList.AddRange(PlayerSpot_v3);
                    }
                    else if (body.valueType == 4)
                    {
                        spotList.AddRange(PlayerSpot_s);
                    }
                }

                if (body.trackType == 2)
                {
                    prop = serializedObject.FindProperty("targetObject");
                    EditorGUILayout.PropertyField(prop);
                    if (body.valueType == 0)
                    {
                        spotList.AddRange(ObjectSpot_b);
                    }
                    else if (body.valueType == 2 || body.valueType == 3)
                    {
                        spotList.AddRange(ObjectSpot_v3);
                    }
                }

                if (body.trackType == 3)
                {
                    if (body.valueType == 1 || body.valueType == 2)
                    {
                        spotList.AddRange(WorldSpot_if);
                    }
                }

                if (body.trackType == 4)
                {
                    prop = serializedObject.FindProperty("targetObject");
                    EditorGUILayout.PropertyField(prop);
                    if (body.targetObject)
                    {
                        body.targetComponent = null;
                        serializedObject.FindProperty("spot").stringValue = "";
                        List<System.Type> UITypes = new List<System.Type>();
                        if (body.valueType == 0)
                        {
                            UITypes.Add(typeof(Toggle));
                        }
                        if (body.valueType == 1)
                        {
                            UITypes.Add(typeof(Text));
                            UITypes.Add(typeof(InputField));
                            UITypes.Add(typeof(Dropdown));
                        }
                        if (body.valueType == 2)
                        {
                            UITypes.Add(typeof(Slider));
                            UITypes.Add(typeof(Scrollbar));
                            UITypes.Add(typeof(Text));
                            UITypes.Add(typeof(InputField));
                            UITypes.Add(typeof(Dropdown));
                        }
                        if (body.valueType == 4)
                        {
                            UITypes.Add(typeof(Text));
                            UITypes.Add(typeof(InputField));
                        }
                        foreach (var type in UITypes)
                        {
                            body.targetComponent = body.targetObject.GetComponent(type);
                            if (body.targetComponent != null)
                            {
                                serializedObject.FindProperty("spot").stringValue = type.Name;
                                break;
                            }
                        }
                        if (body.targetComponent == null)
                        {
                            EditorGUILayout.HelpBox($"{(ValueType)body.valueType} で取得可能な UI コンポーネントがありません。", MessageType.Error);
                        }
                        else
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            prop = serializedObject.FindProperty("targetComponent");
                            EditorGUILayout.PropertyField(prop);
                            EditorGUI.EndDisabledGroup();
                        }
                    }
                }

                if (body.trackType == 5)
                {
                    prop = serializedObject.FindProperty("targetComponent");
                    prop.objectReferenceValue = EditorGUILayout.ObjectField("Animator", prop.objectReferenceValue, typeof(Animator), true);
                    if (prop.objectReferenceValue != null)
                    {
                        Animator animator = prop.objectReferenceValue as Animator;
                        if (animator)
                        {
                            animator.Update(0);
                            for (int i = 0; i < animator.parameters.Length; i++)
                            {
                                var paramType = animator.GetParameter(i).type;
                                if (body.valueType == 0 && paramType == AnimatorControllerParameterType.Bool)
                                {
                                    spotList.Add(animator.GetParameter(i).name);
                                }
                                if (body.valueType == 1 && paramType == AnimatorControllerParameterType.Int)
                                {
                                    spotList.Add(animator.GetParameter(i).name);
                                }
                                if (body.valueType == 2 && paramType == AnimatorControllerParameterType.Float)
                                {
                                    spotList.Add(animator.GetParameter(i).name);
                                }
                            }
                        }
                    }
                }

                if (body.trackType == 6)
                {
                    if (body.valueType == 2)
                    {
                        spotList.AddRange(ControllerSpot_f);
                    }
                }

                if (spotList.Count > 0)
                {
                    var spotIndex = EditorGUILayout.Popup("Spot", spotList.IndexOf(body.spot), spotList.ToArray());
                    serializedObject.FindProperty("spot").stringValue = spotIndex >= 0 ? spotList[spotIndex] : "";
                }

                if (body.valueType == 2)
                {
                    List<string> changeable = new List<string>();
                    changeable.AddRange(PlayerSpot_v3);
                    changeable.AddRange(ObjectSpot_v3);
                    if (changeable.Contains(body.spot))
                    {
                        List<string> detailList = new List<string>(SpotDetail_v3_f);
                        var detailIndex = EditorGUILayout.Popup("Spot Detail", detailList.IndexOf(body.spotDetail), SpotDetail_v3_f);
                        serializedObject.FindProperty("spotDetail").stringValue = detailIndex >= 0 ? SpotDetail_v3_f[detailIndex] : "";
                    }
                }

                if (body.valueType == 4)
                {
                    List<string> detailList = new List<string>(SpotDetail_s);
                    var detailIndex = EditorGUILayout.Popup("Spot Detail", detailList.IndexOf(body.spotDetail), SpotDetail_s);
                    serializedObject.FindProperty("spotDetail").stringValue = detailIndex >= 0 ? SpotDetail_s[detailIndex] : "";
                    if (serializedObject.FindProperty("spotDetail").stringValue == "OneLetter")
                    {
                        prop = serializedObject.FindProperty("index");
                        EditorGUILayout.PropertyField(prop);
                    }
                }

                if (body.spot.Contains("Position"))
                {
                    prop = serializedObject.FindProperty("positionTracking");
                    EditorGUILayout.PropertyField(prop);
                }
                else
                {
                    serializedObject.FindProperty("positionTracking").boolValue = false;
                }

                bool constAlways = body.trackType == 6 && body.valueType == 2;
                EditorGUI.BeginDisabledGroup(constAlways);
                prop = serializedObject.FindProperty("updateEveryFrame");
                EditorGUILayout.PropertyField(prop);
                EditorGUI.EndDisabledGroup();
                if (constAlways) { serializedObject.FindProperty("updateEveryFrame").boolValue = true; }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
