
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnStationExited))]
    internal class T23_OnStationExitedEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("localOnly"));
        }
    }
}
