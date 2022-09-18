
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetVoiceParameters))]
    internal class T23_SetVoiceParametersEditor : T23_ActionEditorBase
    {
        public enum TargetPlayer
        {
            All = 0,
            TriggeredPlayer = 1
        }
        private TargetPlayer targetPlayer;

        protected override void DrawFields()
        {
            var triggeredPlayer_prop = serializedObject.FindProperty("triggeredPlayer");
            triggeredPlayer_prop.boolValue = (TargetPlayer)EditorGUILayout.EnumPopup("Target Player", (TargetPlayer)System.Convert.ToInt32(triggeredPlayer_prop.boolValue)) == TargetPlayer.TriggeredPlayer;
            if (triggeredPlayer_prop.boolValue) { EditorGUILayout.HelpBox("TriggeredPlayer で検出できるのは、Local の Trigger のみです。", MessageType.Info); }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("distanceFar"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("distanceNear"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("gain"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lowpass"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("volumetricRadius"));
        }
    }
}
