
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnPlayerLeft))]
    internal class T23_OnPlayerLeftEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("excludeLocal"));
        }
    }
}
