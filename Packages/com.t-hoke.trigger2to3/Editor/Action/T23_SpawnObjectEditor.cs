#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SpawnObject))]
    internal class T23_SpawnObjectEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.HelpBox("SpawnObject は推奨されません。 VRC_ObjectPool の利用を検討してください。SpawnObjectPool で使用できます。", MessageType.Warning);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prefab"));
            DrawRecieversList("locations");
        }
    }
}
#endif
