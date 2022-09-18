
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnOwnershipTransfer))]
    internal class T23_OnOwnershipTransferEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("localOnly"));
        }
    }
}
