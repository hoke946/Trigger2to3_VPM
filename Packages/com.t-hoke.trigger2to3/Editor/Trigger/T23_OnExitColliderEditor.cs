#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;
using UnityEngine;

namespace Trigger2to3
{
    [CustomEditor(typeof(T23_OnExitCollider))]
    internal class T23_OnExitColliderEditor : T23_TriggerEditorBase
    {
        protected override void DrawFields()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerIndividuals"));
            var layers = serializedObject.FindProperty("layers");
            EditorGUILayout.PropertyField(layers);
            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();
            var includeLocalPlayer = EditorGUILayout.Toggle("Local Player", layers.intValue == (layers.intValue | 1 << LayerMask.NameToLayer("PlayerLocal")));
            if (EditorGUI.EndChangeCheck())
            {
                if (includeLocalPlayer) { layers.intValue = layers.intValue | 1 << LayerMask.NameToLayer("PlayerLocal"); }
                else { layers.intValue = layers.intValue & ~(1 << LayerMask.NameToLayer("PlayerLocal")); }
            }
            EditorGUI.BeginChangeCheck();
            var includePlayer = EditorGUILayout.Toggle("Remote Player", layers.intValue == (layers.intValue | 1 << LayerMask.NameToLayer("Player")));
            if (EditorGUI.EndChangeCheck())
            {
                if (includePlayer) { layers.intValue = layers.intValue | 1 << LayerMask.NameToLayer("Player"); }
                else { layers.intValue = layers.intValue & ~(1 << LayerMask.NameToLayer("Player")); }
            }
            EditorGUI.indentLevel--;
        }
    }
}
#endif
