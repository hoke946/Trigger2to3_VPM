#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_ShuffleObjectPool))]
    internal class T23_ShuffleObjectPoolEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectPool"));
            EditorGUILayout.HelpBox(T23_Localization.GetWord("ObjectPool_ownership"), MessageType.Info);
        }
    }
}
#endif
