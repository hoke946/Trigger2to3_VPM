
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPlayerRespawn))]
    internal class T23_OnPlayerRespawnEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("localOnly"));
        }
    }
}
