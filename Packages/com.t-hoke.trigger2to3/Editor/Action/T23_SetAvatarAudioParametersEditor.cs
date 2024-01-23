#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SetAvatarAudioParameters))]
    internal class T23_SetAvatarAudioParametersEditor : T23_ActionEditorBase
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("gain"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("farRadius"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("nearRadius"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("volumetricRadius"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("forceSpatial"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("customCurve"));
        }
    }
}
#endif
