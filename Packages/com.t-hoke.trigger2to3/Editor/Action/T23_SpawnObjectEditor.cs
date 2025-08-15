#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_SpawnObject))]
    internal class T23_SpawnObjectEditor : T23_ActionEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.HelpBox(T23_Localization.GetWord("SpawnObject_deprecated"), MessageType.Warning);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("prefab"));
            DrawRecieversList("locations");
        }
    }
}
#endif
