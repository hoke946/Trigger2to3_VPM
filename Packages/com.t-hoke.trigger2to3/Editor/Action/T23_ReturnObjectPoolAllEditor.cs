
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ReturnObjectPoolAll))]
    internal class T23_ReturnObjectPoolAllEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectPool"));
            EditorGUILayout.HelpBox("無条件でObject PoolのOwnershipを取得します。", MessageType.Info);
        }
    }
}
